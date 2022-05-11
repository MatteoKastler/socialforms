using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace socialforms.Models.DB.sql {
    public class AnswerQuery : IAnswerQuery {
        private string _connString = "Server=localhost;Port=3308;Database=socialforms;uid=root;pwd=toor"; //den muss ma anpassen an eigene Datenbank
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
        public bool delete(int answId) {
            if ((this._conn == null) || (this._conn.State != ConnectionState.Open)) {
                return false;
            }
            DbCommand cmdDelete = this._conn.CreateCommand();
            cmdDelete.CommandText = "DELETE from answers WHERE answerId = @ansId;";

            DbParameter paramId = cmdDelete.CreateParameter();
            paramId.ParameterName = "ansId";
            paramId.DbType = DbType.Int32;
            paramId.Value = answId;
            cmdDelete.Parameters.Add(paramId);

            return cmdDelete.ExecuteNonQuery() == 1;
        }

        public Answer getAnswer(int answId) {
            if ((this._conn == null) || (this._conn.State != ConnectionState.Open)) {
                return null;
            }
            DbCommand getForm = this._conn.CreateCommand();
            getForm.CommandText = "SELECT * from answers WHERE answerId = @answId";

            DbParameter paramId = getForm.CreateParameter();
            paramId.ParameterName = "answId";
            paramId.DbType = DbType.Int32;
            paramId.Value = answId;

            getForm.Parameters.Add(paramId);
            using (DbDataReader reader = getForm.ExecuteReader()) {
                Answer temp = new Answer {
                    AnswerId = Convert.ToInt32(reader["answerId"]),
                    QuestionId = Convert.ToInt32(reader["questionId"]),
                    UserId = Convert.ToInt32(reader["userId"]),
                    TextAnswer = Convert.ToString(reader["textAnswer"]),
                    ChoiceAnswer = Convert.ToInt32(reader["choiceAnswer"]),
                    SliderAnswer = Convert.ToInt32(reader["sliderAnswer"])
                };
                return temp;
            }
        }

        public bool Insert(Answer a) {
            if ((this._conn == null) || (this._conn.State != ConnectionState.Open)) {
                return false;
            }
            DbCommand cmdInsert = this._conn.CreateCommand();

            cmdInsert.CommandText = "INSERT into answers value(null, @qstId, @userId, @txtAns, @choiceAns, @sliderAns)";

            DbParameter paramId = cmdInsert.CreateParameter();
            paramId.ParameterName = "qstId";
            paramId.DbType = DbType.Int32;
            paramId.Value = a.QuestionId;

            DbParameter paramUId = cmdInsert.CreateParameter();
            paramUId.ParameterName = "userId";
            paramUId.DbType = DbType.Int32;
            paramUId.Value = a.UserId;

            DbParameter paramtext = cmdInsert.CreateParameter();
            paramtext.ParameterName = "txtAns";
            paramtext.DbType = DbType.String;
            paramtext.Value = a.TextAnswer;

            DbParameter paramchoice = cmdInsert.CreateParameter();
            paramchoice.ParameterName = "choiceAns";
            paramchoice.DbType = DbType.String;
            paramchoice.Value = a.ChoiceAnswer;


            DbParameter paramslider = cmdInsert.CreateParameter();
            paramslider.ParameterName = "sliderAns";
            paramslider.DbType = DbType.String;
            paramslider.Value = a.SliderAnswer;

            cmdInsert.Parameters.Add(paramId);
            cmdInsert.Parameters.Add(paramUId);
            cmdInsert.Parameters.Add(paramtext);
            cmdInsert.Parameters.Add(paramchoice);
            cmdInsert.Parameters.Add(paramslider);

            return cmdInsert.ExecuteNonQuery() == 1;    

        }
    }
}
