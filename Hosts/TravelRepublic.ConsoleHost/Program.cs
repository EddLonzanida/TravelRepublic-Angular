using System;
using System.Globalization;
using System.Linq;
using System.Net;
using Eml.DataRepository;
using TravelRepublic.Data.Dto;

namespace TravelRepublic.ConsoleHost
{
    public class Program
    {
        private const string SAMPLE_DATA_SOURCES = @"Migrations\SampleDataSources";

        private const string THUMBNAILS = @"Images\Thumbnails";

        private const string IMAGES = @"Images";

        static void Main(string[] args)
        {
            var hotel = Seeder.GetJsonStub<Hotel>("hotels", SAMPLE_DATA_SOURCES);
            var ctr = 0;

            hotel.Establishments.OrderBy(r => r.Name).ToList().ForEach(r =>
            {
                ctr++;
                using (var webClient = new WebClient())
                {
                    var hasError = false;

                    Console.WriteLine($"Saving images for {r.Name}..");

                    try
                    {
                        webClient.DownloadFile(r.ImageUrl, GetLocalPath(IMAGES, r.ImageUrl));
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(r.ImageUrl);
                        Console.WriteLine(e);

                        hasError = true;
                    }
                    try
                    {
                        webClient.DownloadFile(r.ThumbnailUrl, GetLocalPath(THUMBNAILS, r.ThumbnailUrl));
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(r.ThumbnailUrl);
                        Console.WriteLine(e);

                        hasError = true;
                    }

                    if (!hasError) return;

                    ctr--;

                    Console.WriteLine();
                }
            });

            Console.WriteLine($"{ctr.ToString("N0", CultureInfo.InvariantCulture)} Done.");
            Console.ReadLine();
        }

        private static string GetLocalPath(string relativeDirectory, string url)
        {
            var uri = new Uri(url);
            var absolutePath = uri.AbsolutePath;
            var fn = absolutePath.Replace("/", @"\");
            var fullPath = Seeder.GetFullPath(fn,relativeDirectory);
            //var baseDirectory = Seed.GetFullPath(fn,relativeDirectory);
            //var localPath = $@"{baseDirectory}\{relativeDirectory}\{absolutePath.Replace("/", @"\")}";
            //var destinationDirectory = Path.GetDirectoryName(localPath);

            //if (Directory.Exists(destinationDirectory)) return localPath;

            //if (destinationDirectory != null)
            //{
            //    Directory.CreateDirectory(destinationDirectory);
            //}

            return fullPath;
        }
    }
}
