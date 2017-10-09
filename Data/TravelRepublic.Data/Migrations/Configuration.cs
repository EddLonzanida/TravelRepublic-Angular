using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Data.Entity.Validation;
using System.IO;
using Newtonsoft.Json;
using TravelRepublic.Data.Dto;

namespace TravelRepublic.Data.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<TravelRepublicDb>
    {
        private const string JSON_SOURCES = @"Migrations\JsonSources";

        public Configuration()
        {
            var isEnabled = false; //Disable if running in Release Mode
#if DEBUG
            isEnabled = true;
#endif
            AutomaticMigrationsEnabled = isEnabled;
            AutomaticMigrationDataLossAllowed = isEnabled;
        }

        protected override void Seed(TravelRepublicDb context)
        {
            DoSeed(context);
        }

        private static void DoSeed(TravelRepublicDb context)
        {
            Console.WriteLine("====== Seed start..");

            var fullPath = $@"{AppDomain.CurrentDomain.BaseDirectory}{JSON_SOURCES}\hotels.json";
            var hotel = JsonConvert.DeserializeObject<Hotel>(File.ReadAllText(fullPath));
            hotel.Establishments.ForEach(r =>
            {
                context.Establishments.AddOrUpdate(establishment => establishment.EstablishmentId, r);
            });
        }

        private static void DoSave(DbContext context, string EntityName)
        {
            try
            {
                Console.WriteLine("Saving.. {0}", EntityName);
                context.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        var error = string.Format("===Entity: {2}    Property: {0}       Error: {1}", validationError.PropertyName, validationError.ErrorMessage, EntityName);
                        System.Console.WriteLine(error);
                        throw new Exception(error);
                    }
                }
            }
        }

    }
}
