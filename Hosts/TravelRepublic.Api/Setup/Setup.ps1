#Configure site START
Import-Module WebAdministration
  
function DisplayPadRight($pad, $msg1, $msg2){
    $scrWidth = 80
    $color = "Cyan"
    if ($msg2 -eq $null){
        Write-Host $msg1.PadRight($scrWidth,$pad) -foreground $color
        return
    }
    $padLength = $scrWidth - $msg1.Length - 1
    Write-Host $msg1 $msg2.PadRight($padLength,$pad) -foreground $color
}

function WriteWarning($msg){
    $color = "Magenta"
    Write-Host $msg -foreground $color
}

function DisplayEndingMessage($siteName){
    Write-Host ' '
    DisplayPadRight '▲' "*END $siteName"
}

function CreateHostsEntry($hostName, $ipAdddress){
    Try{
	    if((Get-Command "Remove-HostEntry" -ErrorAction SilentlyContinue) -eq $null) {
		    if((Get-Command "Install-Module" -ErrorAction SilentlyContinue) -eq $null) {
			    (new-object Net.WebClient).DownloadString("http://psget.net/GetPsGet.ps1") | iex
			    Import-Module PsGet
		    }
		    Install-Module PsHosts
	    }

	    Write-Host ' '
        Write-Host 'Creating entry to C:\Windows\System32\drivers\etc\hosts ...'
        $siteExists = Get-HostEntry | where { $_.Name -eq $hostName }
        if($siteExists -ne $null)
        {
            Remove-HostEntry -Name $hostName
        }
        Add-HostEntry $hostName $ipAdddress
        return $true
    }catch{
        return $false
    }
}

function IsDatabaseExists($SQLSvr, $DBName){
    Try{
        [System.Reflection.Assembly]::LoadWithPartialName("Microsoft.SqlServer.Smo") | Out-Null;
                              
        $MySQLObject = new-object Microsoft.SqlServer.Management.Smo.Server $SQLSvr;
        $serverFullName = $MySQLObject.databases| Where { $_.name -match $DBName } | Select-Object -first 1 | SELECT Name
        if($serverFullName -eq $null -or $serverFullName.Length -eq 0) {return $false}
        return $true
    }catch{
        return $false
    }
}

function IsSiteExists($siteName){ 
	if($siteName -eq $null){ 
		Write-Host ' '
        WriteWarning 'IsSiteExists: siteName not supplied..'
        return  $false
    }
    Try{
        Set-Location IIS:\Sites
        $siteExists = Get-ChildItem | Where { $_.Name -eq $siteName }| Select-Object -first 1 | Select Name
        if($siteExists -ne $null) {return $true}
        return $false
    }catch{
        return $false
    }
}

function DeleteSite($siteName){
    $siteExists = IsSiteExists $siteName 
    if($siteExists -eq $true){ 
        Try{
            Write-Host ' '
            Write-Host 'Removing  Site -> '$siteName
            Remove-Website -Name $siteName  
        }catch{
         return $false
        }
    } 
}

function CreateAppPool($appPoolName){
    Write-Host ' '
	if($appPoolName -eq $null){ 
        WriteWarning 'CreateAppPool: appPoolName not supplied..'
        return  $false
    }
    Try{
        Set-Location IIS:\AppPools
        $appPoolExists = Get-ChildItem –Path IIS:\AppPools | Where { $_.Name -eq "$appPoolName" } | Select-Object -first 1 | Select Name
    
        if($appPoolExists -ne $null){ 
            Write-Host 'Deleting AppPool -> '$appPoolName
            Remove-WebAppPool -Name $appPoolName 
        }
    
        Write-Host 'Creating AppPool -> '$appPoolName
        $appPool = New-Item $appPoolName
	    return  $true
    }catch{
        return $false
    }
}

function CreateCertificate($name, $dnsName){
    Write-Host ' '
	if($name -eq $null){ 
        WriteWarning 'CreateCertificate: name not supplied..'
        return  $null
    }
	if($dnsName -eq $null){ 
        WriteWarning 'CreateCertificate: dnsName not supplied..'
        return  $null
    }

    $cert = Get-ChildItem Cert:\LocalMachine\My | Where { $_.FriendlyName -eq "$name" }| Select-Object -first 1  
    if($cert -ne $null){ 
        Write-Host 'Deleting Certificate -> '$name
        Get-ChildItem Cert:\LocalMachine\My | Where { $_.FriendlyName -eq "$name" } | Remove-Item
    }
    Write-Host 'Creating Certificate -> '$name
    $cert = New-SelfSignedCertificate -FriendlyName $name -CertStoreLocation cert:\LocalMachine\My -DnsName $dnsName #"https://gloffice.services.local.dev:312"#$env:computername

    Write-Host ' '
    Write-Host "Adding $name to Trusted Root Certification Authorities store.."

    Get-ChildItem Cert:\LocalMachine\root | Where-Object {$_.Subject -eq "CN=$dnsName"} | Remove-Item
    $certStore = New-Object -TypeName System.Security.Cryptography.X509Certificates.X509Store Root, LocalMachine
    $certStore.Open("MaxAllowed")
    $certStore.Add($cert)
    $certStore.Close()

    return $cert.GetCertHashString()
}

function CreateSslSite($hostName, $siteName, $physicalPath, $port, $thumbprint){
    Write-Host ' '
    if($hostName -eq $null){ 
        WriteWarning 'CreateSslSite: hostName not supplied..'
        return  $false
    }    
    if($siteName -eq $null){ 
        WriteWarning 'CreateSslSite: siteName not supplied..'
        return $false
    }
    if($physicalPath -eq $null){ 
        WriteWarning 'CreateSslSite: physicalPath not supplied..'
        return $false 
    }
    if($port -eq $null){ 
        Write-Host 'CreateSslSite: port not supplied..'
        return  $false
    }
    if($thumbprint -eq $null){ 
        WriteWarning 'CreateSslSite: thumbprint not supplied..'
        return  $false
    }
	Try{
        DeleteSite $siteName
		$localSiteName ="https://${hostName}:$port"
		
		
        Write-Host "Creating Site ->   $localSiteName"
        Write-Host '*PhysicalPath :' $physicalPath
        Set-Location IIS:\Sites
        New-Website -Name $siteName -PhysicalPath $physicalPath -IPAddress "*" -Port "$port" -HostHeader $hostName -ApplicationPool $hostName -Ssl  
        Set-WebConfiguration -Location "$siteName" -Filter 'system.webserver/security/access' -Value "Ssl"

        #attach certificate to the website
        cd IIS:\SslBindings
        $sslBindingExists = Get-ChildItem IIS:\SslBindings | Where { $_.Port -eq $port }| Select-Object -first 1  | Select Port
        if($sslBindingExists -ne $null){ 
            Write-Host ' '
            Write-Host 'Removing SslBindings.. ' $sslBindingExists
            Remove-Item "0.0.0.0!$port"
        }

        Write-Host ' '
        Write-Host 'Attaching SSL Certificate...'
        $webServerCert = Get-ChildItem Cert:\LocalMachine\My\$thumbprint
        $webServerCert | New-Item 0.0.0.0!$port

        Write-Host ' '
        Write-Host 'Starting Site -> '$siteName
        Start-Website -Name $siteName
		
        return $true
    }catch{
        return $false
    }
}

function CreateDbUser($loginName, $password, $databaseName, $instanceName){
	#import SQL Server module
    Write-Host ' '
    Write-Host "Creating DbUser -> [$loginName] for [$databaseName]"
   
    #Write-Host 'Import-Module SQLPS -DisableNameChecking'
	#Import-Module SQLPS -DisableNameChecking 3>$Null #'3' is REDIRECTION OPERATOR for WARNINGS only
    Try{ Import-Module SQLPS -DisableNameChecking 3>$Null }
    Catch{ return  $false }

    if($loginName -eq $null){ 
        WriteWarning 'CreateDbUser: loginName not supplied..'
        return  $false
    }  
    if($password -eq $null){ 
        WriteWarning 'CreateDbUser: password not supplied..'
        return  $false
    } 
    if($databaseName -eq $null){ 
        WriteWarning 'CreateDbUser: databaseName not supplied..'
        return  $false
    } 
    if($instanceName -eq $null){ 
        WriteWarning 'CreateDbUser: instanceName not supplied..'
        return  $false
    } 

	$dbUserName = $loginName
	$roleName = "db_owner"
	
    $isDatabaseExists = IsDatabaseExists $instanceName $databaseName 
    if($isDatabaseExists -eq $false){
        WriteWarning "CreateDbUser: [$databaseName] db does not exists!"
        WriteWarning "Open GLOffice.Services.sln -> Right click GLOffice.Services.WcfHost, set as start-up project -> open Package Manager Console -> In default project dropdown, select GLOffice.Data -> run 'update-database -verbose'"
        return  $false
    }

    $server = New-Object -TypeName Microsoft.SqlServer.Management.Smo.Server -ArgumentList $instanceName
    
    if($server -eq $null){ 
        WriteWarning "CreateDbUser: unable to locate server [$instanceName]"
        return  $false
    }  

	# drop login if it exists
	$database = $server.Databases[$databaseName]
	
	if ($server.Logins.Contains($loginName))  
	{   
		Write-Host("Deleting the existing login [$loginName] from db: $database")		
        Try{ $server.Logins[$loginName].Drop()  }
        Catch{ return  $false }
	}

	$login = New-Object -TypeName Microsoft.SqlServer.Management.Smo.Login -ArgumentList $server, $loginName
	$login.LoginType = [Microsoft.SqlServer.Management.Smo.LoginType]::WindowsUser
	$login.PasswordExpirationEnabled = $false
	$login.Create($password)
	Write-Host("*Login [$loginName] created successfully in db: $database")	
		
	
    if ($database.Users[$dbUserName])
    {
        Write-Host("Dropping user [$dbUserName] from db: $database")
        Try{ $database.Users[$dbUserName].Drop() }
        Catch{ return  $false }        
    }	
	
	$dbUser = New-Object -TypeName Microsoft.SqlServer.Management.Smo.User -ArgumentList $database, $dbUserName
    $dbUser.Login = $loginName
    $dbUser.Create()
    Write-Host("*User $dbUser created successfully in db: $database")	
	
	$dbrole = $database.Roles[$roleName]
    $dbrole.AddMember($dbUserName)
    $dbrole.Alter()
    Write-Host("*User $dbUser successfully added to $roleName role in db: $database")
	
    return $true
}

function Setup(){
	$origDirectory = (get-item $PSScriptRoot)
	$physicalPath = (get-item $PSScriptRoot).parent.FullName
	$siteName = "GLOffice.Services.ApiHost"
	$port = 312
    $ipAdddress = "127.0.0.1"
    $hostName = "$siteName.local.dev"
	$loginName = "IIS APPPOOL\$hostName"
	$databaseName = "GLOffice"
	$instanceName = $env:computername
    $certificateDnsName = 'https://' + $hostName + ':' + $port 
    $certificateName = "GLOffice Certificate"
    $dbPassword = "Denim_123"	
    
    Write-Host ' '
    DisplayPadRight '▼' "*START $siteName"
      
    #create app pool
    $result = CreateAppPool $hostName
	if($result -eq $false){
        DisplayEndingMessage $siteName
		cd $origDirectory
        return
    }

    #create ssl certificate
	$thumbprint = CreateCertificate $certificateName $certificateDnsName
	if($thumbprint -eq $null){
        DisplayEndingMessage $siteName
		cd $origDirectory
        return
    }

	#create ssl website
    $result = CreateSslSite $hostName $siteName $physicalPath $port $thumbprint
	if($result -eq $false){
        DisplayEndingMessage $siteName
		cd $origDirectory
        return
    }

	#create entry in C:\Windows\System32\drivers\etc\hosts
    $result = CreateHostsEntry $hostName $ipAdddress 
    if($result -eq $false){
        DisplayEndingMessage $siteName
		cd $origDirectory
        return
    }
    #create nuget folder
    #set PATH for nuget  VssSessionToken
    #run  nuget.exe sources Add -name GLOfficeNuGet -source https://gloffice.pkgs.visualstudio.com/_packaging/GLOffice/nuget/v3/index.json -username gloffice -password 2pemel2q4fqjwvrkrne7fitrcceixzqrcrqh47vy23j52jwtkqka
  
    #create db user
    $result = CreateDbUser $loginName $dbPassword $databaseName $instanceName
    if($result -eq $false){
        DisplayEndingMessage $siteName
		cd $origDirectory
        return
    }
	Write-Host ' '
	Write-Host '**Setup complete: '$certificateDnsName/swagger/ui/index
    DisplayEndingMessage $siteName    
	cd $origDirectory
}
 #Write-Host (get-item $PSScriptRoot).parent.FullName
#run!
Setup

#Get-Childitem cert:LocalMachineroot -Recurse 
