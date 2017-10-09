using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace TravelRepublic.Data.Utils
{
    public static class SimpleMapper
    {
        public static void PropertyMap<T, T1>(T source, T1 destination)
            where T : class
            where T1 : class
        {
            var sourceProperties = source.GetType().GetProperties().ToList();
            var destinationProperties = destination.GetType().GetProperties().ToList();

            foreach (var sourceProperty in sourceProperties)
            {
                var destinationProperty = destinationProperties.Find(item => item.Name == sourceProperty.Name);

                if (destinationProperty == null) continue;
                try
                {
                    destinationProperty.SetValue(destination, sourceProperty.GetValue(source, null), null);
                }
                catch (ArgumentException)
                {
                }
            }
        }

        public static List<T> CreateListFromTable<T>(this DataTable table)
            where T : new()
        {
            return (from DataRow r in table.Rows select r.GetEntityFromRow<T>()).ToList();
        }

        private static T GetEntityFromRow<T>(this DataRow row)
            where T : new()
        {
            var entity = new T();
            foreach (DataColumn c in row.Table.Columns)
            {
                var p = entity.GetType().GetProperty(c.ColumnName);
                if (p != null && row[c] != DBNull.Value)
                {
                    p.SetValue(entity, row[c], null);
                }
            }
            return entity;
        }
    }
}
