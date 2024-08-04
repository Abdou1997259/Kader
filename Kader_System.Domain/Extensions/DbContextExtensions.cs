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
        /// This extension  method to get lookup of Props (Id - Name) for any type dynamically , Notes : this method can Take any Indicator of Props (Id - Name)
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
        /// <summary>
        /// This extension  method to make soft delete for any prop name  then save changes 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="_context"></param>
        /// <param name="_entity"></param>
        /// <param name="_softDeleteProperty"></param>
        /// <returns>number of rows affected</returns>
        public static async Task<int> SoftDeleteAsync<T>(this DbContext _context, T _entity, string _softDeleteProperty = "IsDeleted",bool IsDeleted = true)
        {
            if (_entity is null)
                return 0;

            var entry = _context.Entry(_entity);
            entry.Property(_softDeleteProperty).CurrentValue = IsDeleted;
            entry.Property("DeleteDate").CurrentValue = DateTime.Now;
            entry.State = EntityState.Modified;
            return await _context.SaveChangesAsync();
        }
        /// <summary>
        /// This extension method to begin - end transaction with commit and rollback 
        /// </summary>
        /// <param name="_context"></param>
        /// <param name="_action"></param>
        public static async void ExecuteInTransaction(this DbContext _context, Action _action)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                _action();
                await _context.SaveChangesAsync();
                transaction.Commit();

            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

    }
}
