using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security;
using System.Web;
using Laba1_sql_injection.Models;

namespace Laba1_sql_injection.Security
{
    public static class VerifyCredentials
    {
        public static bool IsValidLoginSafe(this UserViewModel model, UserContext dbUserContext)
        {
            var passHash = PasswordHash.ComputeHash(model.Password);

            var userExists = dbUserContext.Users.Any(user =>
                (user.UserName == model.UserName || user.Email == model.UserName)
                && user.Password == passHash
                );

            return userExists;
        }

        public static bool IsValidLoginUnsafe(this UserViewModel model)
        {
            bool userExists;
            //const string sqlUser = "EPAM";
            //const string sqlpassword = "Check_m8";
            //const string connectionString = "data source=EPBYGROW0188;initial catalog=laba1;";
            const string sqlUser = "aliaksei";//"EPAM";
            const string sqlpassword = "password";
            const string connectionString = @"data source=PEROVSKIJ-PC\MSSQLSERVERV2;initial catalog=laba1;";

            var secureSqlPassword = new SecureString();
            foreach (var c in sqlpassword)
            {
                secureSqlPassword.AppendChar(c);
            }
            secureSqlPassword.MakeReadOnly();

            using (var connection = new SqlConnection(connectionString, new SqlCredential(sqlUser, secureSqlPassword)))
            {
                connection.Open();

                var passHash = PasswordHash.ComputeHash(model.Password);

                var sqlString = "SELECT * FROM [dbo].[User] WHERE UserName='" + model.UserName + "' and Password='" + passHash + "'";

                var sqlCommand = new SqlCommand(sqlString, connection);

                using (var sqlDataReader = sqlCommand.ExecuteReader())
                {
                    userExists = sqlDataReader.HasRows;
                }
            }

            return userExists;
        }
    }
}