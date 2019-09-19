using Agile.BaseLib.Helpers;
using Agile.BaseLib.IoC;
using Agile.BaseLib.Options;
using Microsoft.Extensions.Options;
using SqlSugar;
using System;
using System.Linq;

namespace Agile.BaseLib.DbContext
{
    public class SugarDbContext
    {
        private SqlSugarClient Db { get; set; }

        private static readonly IOptions<DbContextOption> dbOption = AspectCoreContainer.Resolve<IOptions<DbContextOption>>();

        public SugarDbContext()
        {
            Db = new SqlSugarClient(new ConnectionConfig()
            {
                ConnectionString = dbOption.Value.DbConnectionString,
                DbType = (DbType)Enum.Parse(typeof(DbType), dbOption.Value.Type),
                InitKeyType = InitKeyType.Attribute,
                IsAutoCloseConnection = true
            });
            if (dbOption.Value.IsOutputSql)
            {
                Db.Aop.OnLogExecuting = (sql, para) =>
                {
                    string sqlstr = sql + "\r\n" + Db.Utilities.SerializeObject(para.ToDictionary(it => it.ParameterName, it => it.Value));
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(sqlstr);
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine();
                };
            }
        }

        public SqlSugarClient GetInstance()
        {
            return Db;
        }

        public SimpleClient<T> GetEntityDB<T>() where T : class, new()
        {
            return new SimpleClient<T>(Db);
        }


    }
}
