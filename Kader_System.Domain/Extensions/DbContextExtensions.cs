using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kader_System.Domain.Extensions
{
    public static class DbContextExtensions
    {
        /// <summary>
        /// This extensionm method to get lookup of Props (Id - Name) for any type dynamically , Notes : this method can Take any Indicator of Props (Id - Name)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="queryable"></param>
        /// <param name="IdPropIndicator"></param>
        /// <param name="NamePropIndicator"></param>
        /// <returns>New lookup of of Props (Id - Name) From T Type</returns>
        public static async Task<List<dynamic>> ToDynamicLookUpAsync<T>(this IQueryable<T> queryable, string IdPropIndicator = "Id", string NamePropIndicator = "Name")
        {
            var idProperty = typeof(T).GetProperties().FirstOrDefault(p => p.Name.Contains(IdPropIndicator, StringComparison.OrdinalIgnoreCase));
            var nameProperty = typeof(T).GetProperties().FirstOrDefault(p => p.Name.Contains(NamePropIndicator, StringComparison.OrdinalIgnoreCase));

            if (idProperty != null && nameProperty != null)
            {
                var results = await queryable.ToListAsync();
                var dynamicList = new List<dynamic>();

                foreach (var item in results)
                {
                    dynamic expandoObj = new ExpandoObject();
                    var expandoDict = (IDictionary<string, object>)expandoObj;

                    expandoDict[idProperty.Name] = idProperty.GetValue(item);
                    expandoDict[nameProperty.Name] = nameProperty.GetValue(item);

                    dynamicList.Add(expandoObj);
                }

                return dynamicList;
            }
            else
            {
                return [];
            }
        }
    }
}
