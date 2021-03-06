using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Dapper.Contrib.Extensions;

namespace SImpl.Storage.Repository.Dapper.Helpers
{
    public static class DapperContribExtensions
    {
        /// <summary>
        /// Specify a custom table name mapper based on the POCO type name
        /// </summary>
        private static SqlMapperExtensions.TableNameMapperDelegate TableNameMapper = SqlMapperExtensions.TableNameMapper;
        private static readonly ConcurrentDictionary<RuntimeTypeHandle, string> TypeTableName = new();
        public static SqlMapperExtensions.GetDatabaseTypeDelegate GetDatabaseType =SqlMapperExtensions.GetDatabaseType;
        private static readonly Dictionary<string, ISqlAdapter> AdapterDictionary
            = new Dictionary<string, ISqlAdapter>(6)
            {
                ["sqlconnection"] = new SqlServerAdapter(),
                ["sqlceconnection"] = new SqlCeServerAdapter(),
                ["npgsqlconnection"] = new PostgresAdapter(),
                ["sqliteconnection"] = new SQLiteAdapter(),
                ["mysqlconnection"] = new MySqlAdapter(),
                ["fbconnection"] = new FbAdapter()
            };
        private static readonly ISqlAdapter DefaultAdapter = new SqlServerAdapter();
        public static bool DeleteById<T>(this IDbConnection connection, object id, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            if (id == null)
                throw new ArgumentException("Cannot Delete null Object", nameof(id));

            var type = typeof(T);

            if (type.IsArray)
            {
                type = type.GetElementType();
            }
            else if (type.IsGenericType)
            {
                var typeInfo = type.GetTypeInfo();
                bool implementsGenericIEnumerableOrIsGenericIEnumerable =
                    typeInfo.ImplementedInterfaces.Any(ti => ti.IsGenericType && ti.GetGenericTypeDefinition() == typeof(IEnumerable<>)) ||
                    typeInfo.GetGenericTypeDefinition() == typeof(IEnumerable<>);

                if (implementsGenericIEnumerableOrIsGenericIEnumerable)
                {
                    type = type.GetGenericArguments()[0];
                }
            }

            var name = GetTableName(type);
            var adapter = GetFormatter(connection);
            var sb = new StringBuilder();
            sb.AppendFormat("delete from {0} where ", name);
            adapter.AppendColumnNameEqualsValue(sb, "id"); 
            var deleted = connection.Execute(sb.ToString(), id, transaction, commandTimeout);
            return deleted > 0;
        }
        public static async Task<bool> DeleteByIdAsync<T>(this IDbConnection connection, object id, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            if (id == null)
                throw new ArgumentException("Cannot Delete null Object", nameof(id));

            var type = typeof(T);

            if (type.IsArray)
            {
                type = type.GetElementType();
            }
            else if (type.IsGenericType)
            {
                var typeInfo = type.GetTypeInfo();
                bool implementsGenericIEnumerableOrIsGenericIEnumerable =
                    typeInfo.ImplementedInterfaces.Any(ti => ti.IsGenericType && ti.GetGenericTypeDefinition() == typeof(IEnumerable<>)) ||
                    typeInfo.GetGenericTypeDefinition() == typeof(IEnumerable<>);

                if (implementsGenericIEnumerableOrIsGenericIEnumerable)
                {
                    type = type.GetGenericArguments()[0];
                }
            }

            var name = GetTableName(type);
            var adapter = GetFormatter(connection);
            var sb = new StringBuilder();
            sb.AppendFormat("delete from {0} where ", name);
            adapter.AppendColumnNameEqualsValue(sb, "id"); 
            var deleted = await connection.ExecuteAsync(sb.ToString(), id, transaction, commandTimeout);
            return deleted > 0;
        }
         private static ISqlAdapter GetFormatter(IDbConnection connection)
         {
             var name = GetDatabaseType?.Invoke(connection).ToLower()
                        ?? connection.GetType().Name.ToLower();

             return AdapterDictionary.TryGetValue(name, out var adapter)
                 ? adapter
                 : DefaultAdapter;
         }
         private static string GetTableName(Type type)
         {
             if (TypeTableName.TryGetValue(type.TypeHandle, out string name)) return name;

             if (TableNameMapper != null)
             {
                 name = TableNameMapper(type);
             }
             else
             {
                 //NOTE: This as dynamic trick falls back to handle both our own Table-attribute as well as the one in EntityFramework 
                 var tableAttrName =
                     type.GetCustomAttribute<TableAttribute>(false)?.Name
                     ?? (type.GetCustomAttributes(false).FirstOrDefault(attr => attr.GetType().Name == "TableAttribute") as dynamic)?.Name;

                 if (tableAttrName != null)
                 {
                     name = tableAttrName;
                 }
                 else
                 {
                     name = type.Name + "s";
                     if (type.IsInterface && name.StartsWith("I"))
                         name = name.Substring(1);
                 }
             }

             TypeTableName[type.TypeHandle] = name;
             return name;
         }

    }
}