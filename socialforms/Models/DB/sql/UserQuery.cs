using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;

namespace socialforms.Models.DB
{
    public class Userquery : IUsersquery
    {  
        private string _connString = "Server=localhost;Port=5040;Database=socialforms;uid=root;pwd=MBigubb75#";
        DbConnection _conn;


       public void Connect()
        {
            if (this._conn == null) {
                this._conn = new MySqlConnection(this._connString);
                Debug.WriteLine("Userquery: connection has been created");
            }
            if(this._conn.State != System.Data.ConnectionState.Open)
            {
                this._conn.Open();
                Debug.WriteLine("Userquery: state has been set to open");
            }
        } 

        public bool Delete(int userId)
        {
            if ((this._conn == null) || (this._conn.State != ConnectionState.Open))
            {
                return false;
            }
            DbCommand cmdDelete = this._conn.CreateCommand();
            cmdDelete.CommandText = "DELETE from users WHERE userId = @userId;";

            DbParameter paramId = cmdDelete.CreateParameter();
            paramId.ParameterName = "userId";
            paramId.DbType = DbType.Int32;
            paramId.Value = userId;
            cmdDelete.Parameters.Add(paramId);

            return cmdDelete.ExecuteNonQuery() == 1;
        }

        public void Disconnect()
        {
            if((this._conn != null) && (this._conn.State == System.Data.ConnectionState.Open)){
                this._conn.Close();
            }
        }


        //not checked
        public List<User> GetAllUsers()
        {  
            List<User> users = new List<User>();

            if ((this._conn == null) || (this._conn.State != ConnectionState.Open)){ 
                return null;
            }
            DbCommand cmdAllUsers = this._conn.CreateCommand();
            cmdAllUsers.CommandText = "SELECT * from users";
            using (DbDataReader reader = cmdAllUsers.ExecuteReader())
            {
                while (reader.Read()){
                    users.Add(new User {
                        PersonId = Convert.ToInt32(reader["userId"]),
                        Username = Convert.ToString(reader["userName"]),
                        Birthdate = Convert.ToDateTime(reader["birthDate"]),
                        Gender = (Gender)Convert.ToInt32(reader["gender"]),
                        Email = Convert.ToString(reader["email"])
                    });
                }
            }  
            return users;
        }

        public User GetUser(int userId)
        {
            if ((this._conn == null) || (this._conn.State != ConnectionState.Open)) {
                return null;
            }
            DbCommand QUser = this._conn.CreateCommand();
            QUser.CommandText = "SELECT * from users WHERE userId = @userId";

            DbParameter paramId = QUser.CreateParameter();
            paramId.ParameterName = "userId";
            paramId.DbType = DbType.Int32;
            paramId.Value = userId;

            QUser.Parameters.Add(paramId);
            using (DbDataReader reader = QUser.ExecuteReader()) {
                if (reader.HasRows) {
                    reader.Read();
                    User temp = new User {
                        PersonId = Convert.ToInt32(reader["userId"]),
                        Username = Convert.ToString(reader["userName"]),
                        Birthdate = Convert.ToDateTime(reader["birthDate"]),
                        Gender = (Gender)Convert.ToInt32(reader["gender"]),
                        Email = Convert.ToString(reader["email"])
                    };
                    return temp;
                } else { return null; }
            }  
        }

        public bool Insert(User user)
        {  
            if((this._conn == null) || (this._conn.State != ConnectionState.Open))
            {
                return false;
            }
            DbCommand cmdInsert = this._conn.CreateCommand();

            cmdInsert.CommandText = "INSERT into users value(null, @username, sha2(@pwd,256), @bDate, @mail, @gender, null, null)"; 

            DbParameter paramUN = cmdInsert.CreateParameter();
            paramUN.ParameterName = "username";
            paramUN.DbType = DbType.String;
            paramUN.Value = user.Username;

            DbParameter paramPWD = cmdInsert.CreateParameter();
            paramPWD.ParameterName = "pwd";
            paramPWD.DbType = DbType.String;
            paramPWD.Value = user.Password;

            DbParameter paramBDate = cmdInsert.CreateParameter();
            paramBDate.ParameterName = "bDate";
            paramBDate.DbType = DbType.Date;
            paramBDate.Value = user.Birthdate;

            DbParameter paramEMail = cmdInsert.CreateParameter();
            paramEMail.ParameterName = "mail";
            paramEMail.DbType = DbType.String;
            paramEMail.Value = user.Email;

            DbParameter paramGender = cmdInsert.CreateParameter();
            paramGender.ParameterName = "gender";
            paramGender.DbType = DbType.Int32;
            paramGender.Value = user.Gender;

            cmdInsert.Parameters.Add(paramUN)   ;
            cmdInsert.Parameters.Add(paramPWD);
            cmdInsert.Parameters.Add(paramBDate);
            cmdInsert.Parameters.Add(paramEMail);
            cmdInsert.Parameters.Add(paramGender);

            return cmdInsert.ExecuteNonQuery() == 1;
        }

        public User Login(string username, string password)
        {
            if ((this._conn == null) || (this._conn.State != ConnectionState.Open)) {
                return null;
            }
            DbCommand login = this._conn.CreateCommand();

            login.CommandText = "SELECT userId,pass FROM users WHERE userName = @user";

            DbParameter paramUN = login.CreateParameter();
            paramUN.ParameterName = "user";
            paramUN.DbType = DbType.String;
            paramUN.Value = username;

            login.Parameters.Add(paramUN);
                DbDataReader reader = login.ExecuteReader();
                reader.Read();
                int tmp = Convert.ToInt32(reader["userId"]);
                if (String.Equals(reader["pass"], ComputeSha256Hash(password))) {
                    reader.Close();
                    return GetUser(tmp);
                } else {
                    Console.WriteLine("password is not correct");  //TODO: muss auf da website dann iwo hin
                }
            Console.WriteLine("multiple or no user found");  //TODO: muss auch auf die website
            return null;
        }


        public bool Update(int userId, User newUserData)
        {
            //falls die Verb. nicht existiert oder nicht geöffnet ist
            if ((this._conn == null) || (this._conn.State != ConnectionState.Open))
            {
                return false;
            }
            // wir erzeugen unseren SQL-Befehl
            //      leeres Command erzeugen
            DbCommand cmdUpdate = this._conn.CreateCommand();
            cmdUpdate.CommandText = "update users set username = @username, password = sha2(@paramPWD, 512), birthdate = @bDate, email = @mail, gender = @gender where user_id = @id;";

            // leeren Parameter erzeugen
            DbParameter paramUN = cmdUpdate.CreateParameter();
            // hier den selbstgewählten Parameternamen verwenden
            paramUN.ParameterName = "username";
            paramUN.DbType = DbType.String;
            paramUN.Value = newUserData.Username;

            DbParameter paramPWD = cmdUpdate.CreateParameter();
            paramPWD.ParameterName = "paramPWD";
            paramPWD.DbType = DbType.String;
            paramPWD.Value = newUserData.Password;

            DbParameter paramBDate = cmdUpdate.CreateParameter();
            paramBDate.ParameterName = "bDate";
            paramBDate.DbType = DbType.Date;
            paramBDate.Value = newUserData.Birthdate;

            DbParameter paramEMail = cmdUpdate.CreateParameter();
            paramEMail.ParameterName = "mail";
            paramEMail.DbType = DbType.String;
            paramEMail.Value = newUserData.Email;

            DbParameter paramGender = cmdUpdate.CreateParameter();
            paramGender.ParameterName = "gender";
            paramGender.DbType = DbType.Int32;
            paramGender.Value = newUserData.Gender;

            DbParameter paramId = cmdUpdate.CreateParameter();
            paramId.ParameterName = "id";
            paramId.DbType = DbType.Int32;
            paramId.Value = userId;

            //die Parameter mit dem Command verbinden
            cmdUpdate.Parameters.Add(paramUN);
            cmdUpdate.Parameters.Add(paramPWD);
            cmdUpdate.Parameters.Add(paramBDate);
            cmdUpdate.Parameters.Add(paramEMail);
            cmdUpdate.Parameters.Add(paramGender);
            cmdUpdate.Parameters.Add(paramId);

            // nun kann der SQL-Befehl an den DB-Server gesendet werden 
            return cmdUpdate.ExecuteNonQuery() == 1;
        }
        static string ComputeSha256Hash(string rawData) {
            // Create a SHA256   
            using (SHA256 sha256Hash = SHA256.Create()) {
                // ComputeHash - returns byte array  
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                // Convert byte array to a string   
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++) {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }   
    }
}