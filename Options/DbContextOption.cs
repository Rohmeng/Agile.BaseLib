using SqlSugar;

namespace Agile.BaseLib.Options
{
    public class DbContextOption
    {
        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        public string DbConnectionString { get; set; }

        /// <summary>
        /// 数据库类型 默认SqlServer,其它(MySql/Sqlite/Oracle/PostgreSQL)
        /// </summary>
        /// <value></value>
        public string Type {get; set;} = "SqlServer";

        /// <summary>
        /// 是否在控制台输出SQL语句，默认开启
        /// </summary>
        public bool IsOutputSql { get; set; } = true;
    }
}
