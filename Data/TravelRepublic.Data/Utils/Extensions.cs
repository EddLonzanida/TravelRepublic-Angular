using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using TravelRepublic.Business.Common.BaseClasses;
using TravelRepublic.Data.Dto;
using X.PagedList;

namespace TravelRepublic.Data.Utils
{
    public static class Extensions
    {
        public static async Task<IEnumerable<SelectListItem>> ToSelectListItemsAsync<T, Name, Value>(this DbSet<T> items,
            Func<T, Value> valueSelector, Func<T, Name> nameSelector)
            where T : class
        {
            var tmpList = await items.AsEnumerable()
                .OrderBy(nameSelector)
                .ToListAsync();

            var selectListItems = tmpList.Select(r => new SelectListItem
            {
                Text = nameSelector(r).ToString(),
                Value = valueSelector(r).ToString()   //valueSelector
            }).ToList();


            selectListItems.Insert(0, new SelectListItem { Text = "- Select - ", Value = "" });
            return selectListItems;
        }

        public static IEnumerable<SelectListItem> ToSelectListItems<T, Name, Value>(this DbSet<T> items,
            Func<T, Value> valueSelector, Func<T, Name> nameSelector)
            where T : class
        {
            var tmpList =  items.AsEnumerable()
                .OrderBy(nameSelector)
                .ToList();

            var selectListItems = tmpList.Select(r => new SelectListItem
            {
                Text = nameSelector(r).ToString(),
                Value = valueSelector(r).ToString()   //valueSelector
            }).ToList();


            selectListItems.Insert(0, new SelectListItem { Text = "- Select - ", Value = "" });
            return selectListItems;
        }

        public static async Task<IEnumerable<SelectListItem>> ToSelectListItemsAsync<T, Name, Value>(this IEnumerable<T> items,
            Func<T, Value> valueSelector, Func<T, Name> nameSelector)
            where T : class
        {
            var tmpList = await items.AsEnumerable()
                .OrderBy(nameSelector)
                .ToListAsync();

            var selectListItems = tmpList.Select(item => new SelectListItem
                {
                    Text = nameSelector(item).ToString(),
                    Value = valueSelector(item).ToString()   //valueSelector
                }).ToList();

            selectListItems.Insert(0, new SelectListItem { Text = "- Select - ", Value = "" });
            return selectListItems;
        }

        /// <summary>
        /// Do not use if the valueSelector is pointing to another table.
        /// Use include
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="Value"></typeparam>
        /// <param name="items"></param>
        /// <param name="idPredicate"></param>
        /// <param name="valueSelector"></param>
        /// <returns></returns>
        public static async Task<string> GetValueAsync<T, Value>(this DbSet<T> items,
            Func<T, bool> idPredicate,
            Func<T, Value> valueSelector)
            where T : class
        {
            var results = await items
                .Where(idPredicate)
                .Select(valueSelector)
                .ToListAsync();

            var firstOrDefault = results.FirstOrDefault();
            if (firstOrDefault != null) return results.Count > 0 ? firstOrDefault.ToString() : "";
            return null;
        }

        /// <summary>
        /// Used in Creating Lookup table rows
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="Value"></typeparam>
        /// <param name="items"></param>
        /// <param name="valueSelector"></param>
        /// <param name="newItem"></param>
        /// <returns></returns>
        public static async Task<bool> HasDuplicatesAsync<T, Value>(this DbSet<T> items,
            Func<T, Value> valueSelector,
            T newItem)
            where T : EntityBase
        {
            var newValue = valueSelector(newItem).ToString();
            var results =await items.AsEnumerable()
                .Where(x => !string.IsNullOrEmpty(valueSelector(x).ToString())
                            && valueSelector(x).ToString().Equals(newValue, StringComparison.OrdinalIgnoreCase)
                            && x.Id != newItem.Id)
                .ToListAsync();
            return results.Count > 0;
        }

        /// <summary>
        /// Used in Edit
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="Value"></typeparam>
        /// <param name="items"></param>
        /// <param name="valueSelector"></param>
        /// <param name="parentIDSelector"></param>
        /// <param name="newItem"></param>
        /// <returns></returns>
        public static async Task<bool> HasDuplicatesAsync<T, Value>(this DbSet<T> items,
            Func<T, Value> valueSelector,
            Func<T, Value> parentIDSelector,
            T newItem)
            where T : EntityBase
        {
            var newValue = valueSelector(newItem).ToString();
            var parentValue = parentIDSelector(newItem).ToString();

            var results = await items.AsEnumerable()
                .Where(x => !string.IsNullOrEmpty(valueSelector(x).ToString())
                            && valueSelector(x).ToString().Equals(newValue, StringComparison.OrdinalIgnoreCase)
                            && parentIDSelector(x).ToString().Equals(parentValue, StringComparison.OrdinalIgnoreCase)
                            && x.Id != newItem.Id)
                .ToListAsync();
            return results.Count > 0;
        }

        public static void TrimStringValues<T>(this T item)
        {
            var objType = item.GetType();
            var props = objType.GetProperties();
            foreach (var p in props)
            {
                var propertyType = p.PropertyType.Name;
                if (propertyType != "String") continue;

                if (!p.CanWrite) continue;

                var oValue = p.GetValue(item);
                if (oValue == null) continue;

                var propertyValue = oValue.ToString().Trim();
                p.SetValue(item, propertyValue);
            }
        }
    }
}
