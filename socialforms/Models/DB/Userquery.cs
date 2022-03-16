using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

namespace socialforms.Models.DB
{
    public class Userquery : IUsersquery
    {  
        private string _connString = "Server=localhost;database=;user=root;password=MBigubb75#";
        DbConnection _conn;


       public void Connect()
        {
            if(this._conn == null)
            {  
                this._conn = new MySqlConnection(this._connString);
            }   
            if(this._conn.State != System.Data.ConnectionState.Open)
            {  
                this._conn.Open();
            }
        } 

        public bool Delete(int uID)
        {
            //falls die Verb. nicht existiert oder nicht geöffnet ist
            if ((this._conn == null) || (this._conn.State != ConnectionState.Open))
            {
                return false;
            }
            // wir erzeugen unseren SQL-Befehl
            //      leeres Command erzeugen
            DbCommand cmdDelete = this._conn.CreateCommand();
            cmdDelete.CommandText = "delete from users where userId = uID;";

            DbParameter paramId = cmdDelete.CreateParameter();
            paramId.ParameterName = "uID";
            paramId.DbType = DbType.Int32;
            paramId.Value = uID;
            cmdDelete.Parameters.Add(paramId);

            return cmdDelete.ExecuteNonQuery() == 1;
        }

        public void Disconnect()
        {
            if((this._conn != null) && (this._conn.State == System.Data.ConnectionState.Open))
            {
                this._conn.Close();
            }
        }


        //not checked
        public List<User> GetAllUsers()
        {   //leere Liste für die User erzeugen
            List<User> users = new List<User>();

            if ((this._conn == null) || (this._conn.State != ConnectionState.Open))
            {   //null zurückliefern 
                return null;
            }
            DbCommand cmdAllUsers = this._conn.CreateCommand();
            cmdAllUsers.CommandText = "select * from users";
            // ExecuteReader() ... bei SELECT-Abfragen
            // using ... kurze Schreibweise für try ... finally
            using (DbDataReader reader = cmdAllUsers.ExecuteReader())
            {   //Read() ... liest einen Datensatz aus der Tabelle user
                // Read() gibt true zurück, falls ein Datensatz existiert
                //             false zurück, falls keiner mehr existiert
                while (reader.Read())
                {
                    users.Add(new User {
                        PersonId = Convert.ToInt32(reader["user_id"]),
                        Username = Convert.ToString(reader["username"]),
                        Birthdate = Convert.ToDateTime(reader["birthdate"]),
                        Gender = (Gender)Convert.ToInt32(reader["gender"]),
                        Email = Convert.ToString(reader["email"])
                        // Passwort wird nicht ausgelesen
                    });
                }

            }  
            return users;
        }

        public User GetUser(int userId)
        {
            throw new NotImplementedException();
        }

        public bool Insert(User user)
        {  
            if((this._conn == null) || (this._conn.State != ConnectionState.Open))
            {
                return false;
            }
            DbCommand cmdInsert = this._conn.CreateCommand();

            cmdInsert.CommandText = "insert into users value(null, @username, sha2(@pass, 512), @bDate, @mail, @gender)"; // null únd SQL macht dann autoincrement?

            // leeren Parameter erzeugen
            DbParameter paramUN = cmdInsert.CreateParameter();
            // hier den selbstgewählten Parameternamen verwenden
            paramUN.ParameterName = "username";
            paramUN.DbType = DbType.String;
            paramUN.Value = user.Username;

            DbParameter paramPWD = cmdInsert.CreateParameter();
            paramPWD.ParameterName = "pass";
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

            //die Parameter mit dem Command verbinden
            cmdInsert.Parameters.Add(paramUN)   ;
            cmdInsert.Parameters.Add(paramPWD);
            cmdInsert.Parameters.Add(paramBDate);
            cmdInsert.Parameters.Add(paramEMail);
            cmdInsert.Parameters.Add(paramGender);

            // nun kann der SQL-Befehl an den DB-Server gesendet werden 
            return cmdInsert.ExecuteNonQuery() == 1;
        }

        public User Login(string username, string password)
        {
            throw new NotImplementedException();
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
    }
}