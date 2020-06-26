using Mocks.Utils;
using MySql.Data.MySqlClient;
using System;
using Dapper;
using Newtonsoft.Json;

namespace Mocks.Models
{
    public class Pass
    {
        public string Guid { get; set; }
        public string PersonName { get; set; }
        public string PersonSurname { get; set; }
        public string PersonPatronymic { get; set; }
        public string PassportNumber { get; set; }
        public string DateFrom { get; set; }
        public string DateTo { get; set; }

        public static string GetPass(string guid)
        {
            using (var connection = DBUtils.DBConnection())
            {
                var sql = $"SELECT * FROM passes WHERE GUID = {guid}";

                connection.Open();
                var result = connection.QuerySingle<Pass>(sql);
                return JsonConvert.SerializeObject(result);
            }
        }
    }
}
