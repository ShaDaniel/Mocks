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

        /*
         * ����-������� ��� ��������
         */
        private static string ToJson(object data)
        {
            return JsonConvert.SerializeObject(data, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.Ignore });
        }

        public enum Valid
        {
            Notfound,
            Valid,
            Invalid
        }
        /*
         * 
         */
        public static Pass GetPass(string guid)
        {
            using (var connection = DBUtils.DBConnection())
            {
                var sql = $"SELECT GUID, PersonName, PersonSurname, PersonPatronymic, PassportNumber FROM passes WHERE GUID = '{guid}'";

                connection.Open();
                var result = connection.QuerySingleOrDefault<Pass>(sql);
                if (result == null)
                    return null;
                return result;
            }
        }

        public static Pass SavePass(Pass pass)
        {
            var guid = System.Guid.NewGuid().ToString();
            using (var connection = DBUtils.DBConnection())
            {
                // �������� ���������� ���� ����� values
                var sql = @$"INSERT INTO passes 
                        (guid, personName, personSurname, personPatronymic,
                        passportNumber, dateFrom, dateTo) VALUES ('{guid}', '{pass.PersonName}',
                                                   '{pass.PersonSurname}', '{pass.PersonPatronymic}',
                                                   '{pass.PassportNumber}', '{DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss")}',
                                                   '{DateTime.Now.AddDays(2).ToString("yyyy-MM-dd hh:mm:ss")}')";
                connection.Open();
                connection.Execute(sql);
            }
            return new Pass { Guid = guid };
        }

        public static bool UpdatePass(Pass pass)
        {
            using (var connection = DBUtils.DBConnection())
            {
                var sql = $"UPDATE passes SET GUID = '{pass.Guid}'";
                if (pass.PersonName != null)
                    sql += $", PersonName = '{pass.PersonName}'";
                if (pass.PersonSurname != null)
                    sql += $", PersonSurname = '{pass.PersonSurname}'";
                if (pass.PersonPatronymic != null)
                    sql += $", PersonPatronymic = '{pass.PersonPatronymic}'";
                if (pass.PassportNumber != null)
                    sql += $", PassportNumber = '{pass.PassportNumber}'";
                sql += $" WHERE GUID = '{pass.Guid}'";

                connection.Open();
                var res = connection.Execute(sql);
                return Convert.ToBoolean(res);
            }
        }
        public static bool DeletePass(string guid)
        {
            using (var connection = DBUtils.DBConnection())
            {
                var sql = $"DELETE FROM passes WHERE GUID = '{guid}'";

                connection.Open();
                var res = connection.Execute(sql);
                return Convert.ToBoolean(res);
            }
        }

        public static Valid ValidatePass(string guid)
        {
            using (var connection = DBUtils.DBConnection())
            {
                var sql = $"SELECT DateTo FROM passes WHERE GUID = '{guid}'";

                connection.Open();
                var date = connection.QuerySingleOrDefault<DateTime?>(sql);
                if (date == null)
                    return Valid.Notfound;
                if (date < DateTime.Now)
                    return Valid.Invalid;
                return Valid.Valid;
            }
        }
    }
}
