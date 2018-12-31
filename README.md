# TravelRepublic
Sample SOA using .NetCore2.2 with Angular7 on the front-end. Featuring hotels and booking style searches.

## Seed the database
1. Compile and build
2. Right click TravelRepublic project and Set as startup project
3. Open Package manager console
4. In the 'Default project' drop down, select TravelRepublic.DataMigration
5. In the console, type update-database -verbose
4. press enter

## Run the application
1. In the Standard toolbar, select TravelRepublic.Api from the dropdown list, replacing the default IIS Express
2. Press F5 to run the back-end webapi using Kestrel
3. Open powershell
4. Navigate to TravelRepublic\Hosts\TravelRepublic.Spa
5. type: npm start
6. press enter

### Quick View
![](https://github.com/EddLonzanida/TravelRepublic-Angular/blob/master/Docs/Art/MainScreen.png)