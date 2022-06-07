using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace socialforms.Models.DB.sql {
    public class QuestionQuery : IQuestionQuery {
        private string _connString = "Server=localhost;Port=3308;Database=socialforms;uid=root;pwd=toor";
        DbConnection _conn;

        public void Connect() {
            if (this._conn == null) {
                this._conn = new MySqlConnection(this._connString);
                Debug.WriteLine("connection has been created");
            }
            if (this._conn.State != System.Data.ConnectionState.Open) {
                this._conn.Open();
                Debug.WriteLine("state has been set to open");
            }
        }

        public void Disconnect() {
            if ((this._conn != null) && (this._conn.State == System.Data.ConnectionState.Open)) {
                this._conn.Close();
            }
        }
        public bool delete(int qstId) {
            if ((this._conn == null) || (this._conn.State != ConnectionState.Open)) {
                return false;
            }
            DbCommand cmdDelete = this._conn.CreateCommand();
            cmdDelete.CommandText = "DELETE from questions WHERE questionId = @qstId;";

            DbParameter paramId = cmdDelete.CreateParameter();
            paramId.ParameterName = "qstId";
            paramId.DbType = DbType.Int32;
            paramId.Value = qstId;
            cmdDelete.Parameters.Add(paramId);

            return cmdDelete.ExecuteNonQuery() == 1;
        }

        public Question getQuestion(int qstId) {
            if ((this._conn == null) || (this._conn.State != ConnectionState.Open)) {
                return null;
            }
            DbCommand getForm = this._conn.CreateCommand();
            getForm.CommandText = "SELECT * from questions WHERE qstId = @qstId";

            DbParameter paramId = getForm.CreateParameter();
            paramId.ParameterName = "qstId";
            paramId.DbType = DbType.Int32;
            paramId.Value = qstId;

            getForm.Parameters.Add(paramId);
            using (DbDataReader reader = getForm.ExecuteReader()) {
                reader.Read();
                Question temp = new Question
                {
                    QuestionId = Convert.ToInt32(reader["questionId"]),
                    FormId = Convert.ToInt32(reader["formId"]),
                    Qtext = Convert.ToString(reader["text"]),
                    QuestionType = Convert.ToInt32(reader["questionType"])
                };
                return temp;
            }
        }


        public bool Insert(Question q) {
            if ((this._conn == null) || (this._conn.State != ConnectionState.Open)) {
                return false;
            }
            DbCommand cmdInsert = this._conn.CreateCommand();

            cmdInsert.CommandText = "INSERT into questions value(null, @formId, @text, @qsttype)";

            DbParameter paramId = cmdInsert.CreateParameter();
            paramId.ParameterName = "formId";
            paramId.DbType = DbType.Int32;
            paramId.Value = q.FormId;

            DbParameter paramText = cmdInsert.CreateParameter();
            paramText.ParameterName = "text";
            paramText.DbType = DbType.String;
            paramText.Value = q.Qtext;

            DbParameter paramType = cmdInsert.CreateParameter();
            paramType.ParameterName = "qsttype";
            paramType.DbType = DbType.Date;
            paramType.Value = q.QuestionType;


            cmdInsert.Parameters.Add(paramId);
            cmdInsert.Parameters.Add(paramType);
            cmdInsert.Parameters.Add(paramText);

            return cmdInsert.ExecuteNonQuery() == 1;
        }
    }
}
