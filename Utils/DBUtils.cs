using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mocks.Utils
{
    static class DBUtils
    {
        private static string Host { get; } = "127.0.0.1";
        private static string Database { get; } = "autotests";
        private static string Port { get; } = "3306";
        private static string Username { get; } = "root";
        private static string Password { get; } = "кщще";
        private static string Charset { get; } = "cp866";
        public static MySqlConnection DBConnection()
        {
            var concat = "Server=" + Host + ";Database=" + Database + ";Port="
                         + Port + ";User Id=" + Username + ";Password=" + Password + ";Charset=" + Charset;

            var myConnection = new MySqlConnection(concat);
            return myConnection;
        }
    }
}
