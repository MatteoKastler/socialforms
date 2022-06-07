using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace socialforms.Models.DB.sql
{
    public class FormQuery : IFormQuery {

        private string _connString = "Server=localhost;Port=3306;Database=socialforms;uid=root;pwd=MBigubb75#";
        DbConnection _conn;

        public bool Connect()
        {
            if (this._conn == null)
            {
                this._conn = new MySqlConnection(this._connString);
                Debug.WriteLine("FormQuery: connection has been created");
            }
            if (this._conn.State != System.Data.ConnectionState.Open)
            {
                this._conn.Open();
                Debug.WriteLine("FormQuery: state has been set to open");
            }
            if (this._conn.State == System.Data.ConnectionState.Open) {
                return true;
            } else { return false; }
        }

        public void Disconnect()
        {
            if ((this._conn != null) && (this._conn.State == System.Data.ConnectionState.Open))
            {
                this._conn.Close();
            }
        }


        public int cntQuestions(int formId) {
            if ((this._conn == null) || (this._conn.State != ConnectionState.Open)) {
                return -1;
            }
            DbCommand getForm = this._conn.CreateCommand();
            getForm.CommandText = "SELECT COUNT(*) AS questioncnt from questions WHERE formId = @formId";

            DbParameter paramId = getForm.CreateParameter();
            paramId.ParameterName = "FormId";
            paramId.DbType = DbType.Int32;
            paramId.Value = formId;

            getForm.Parameters.Add(paramId);

            using (DbDataReader reader = getForm.ExecuteReader()) {
                reader.Read();
                return Convert.ToInt32(reader["questioncnt"]);
            }
        }

        public int cntUseranswers(int formId) {
            if ((this._conn == null) || (this._conn.State != ConnectionState.Open)) {
                return -1;
            }
            DbCommand getForm = this._conn.CreateCommand();
            getForm.CommandText = "SELECT COUNT(*) AS cntanswers from answers WHERE questionId = @formId";

            DbParameter paramId = getForm.CreateParameter();
            paramId.ParameterName = "FormId";
            paramId.DbType = DbType.Int32;
            paramId.Value = formId;

            getForm.Parameters.Add(paramId);

            using (DbDataReader reader = getForm.ExecuteReader()) {
                reader.Read();
                return Convert.ToInt32(reader["cntanswers"]);
            }
        }

        public bool Delete(int formId) {
            if ((this._conn == null) || (this._conn.State != ConnectionState.Open)) {
                return false;
            }
            DbCommand cmdDelete = this._conn.CreateCommand();
            cmdDelete.CommandText = "DELETE from forms WHERE formId = @formId;";

            DbParameter paramId = cmdDelete.CreateParameter();
            paramId.ParameterName = "formId";
            paramId.DbType = DbType.Int32;
            paramId.Value = formId;
            cmdDelete.Parameters.Add(paramId);

            return cmdDelete.ExecuteNonQuery() == 1;
        }

        public Form getForm(int formId) {
            if ((this._conn == null) || (this._conn.State != ConnectionState.Open)) {
                return null;
            }
            DbCommand getForm = this._conn.CreateCommand();
            getForm.CommandText = "SELECT * from forms WHERE formId = @formId";

            DbParameter paramId = getForm.CreateParameter();
            paramId.ParameterName = "FormId";
            paramId.DbType = DbType.Int32;
            paramId.Value = formId;

            getForm.Parameters.Add(paramId);
            using (DbDataReader reader = getForm.ExecuteReader()) {
                reader.Read();
                Form temp = new Form { 
                    FormId = Convert.ToInt32(reader["formId"]),
                    UserId = Convert.ToInt32(reader["userId"]),
                    FormName = Convert.ToString(reader["formName"]),
                    CreateDate = Convert.ToDateTime(reader["createDate"])
                };
                return temp;
            }
        }

        public Form findByName(String text, int userId)
        {
            if ((this._conn == null) || (this._conn.State != ConnectionState.Open))
            {
                return null;
            }
            DbCommand getForm = this._conn.CreateCommand();
            getForm.CommandText = "SELECT * from forms WHERE text = @text and userId = @userId";

            DbParameter paramText = getForm.CreateParameter();
            paramText.ParameterName = "text";
            paramText.DbType = DbType.Int32;
            paramText.Value = text;

            DbParameter paramId = getForm.CreateParameter();
            paramId.ParameterName = "userId";
            paramId.DbType = DbType.Int32;
            paramId.Value = userId;

            getForm.Parameters.Add(paramId);
            getForm.Parameters.Add(paramText);
            getForm.Parameters.Add(paramText);
            //Debug.WriteLine(Form.toString());
            using (DbDataReader reader = getForm.ExecuteReader())
            {
                reader.Read();
                Form temp = new Form
                {
                    FormId = Convert.ToInt32(reader["formId"]),
                    UserId = Convert.ToInt32(reader["userId"]),
                    FormName = Convert.ToString(reader["formName"]),
                    CreateDate = Convert.ToDateTime(reader["createDate"])
                };
                return temp;
            }
        }

        public List<Form> getForms(int userId) {
            List<Form> forms = new List<Form>();

            if ((this._conn == null) || (this._conn.State != ConnectionState.Open)) {
                return null;
            }
            DbCommand cmdAllForms = this._conn.CreateCommand();
            cmdAllForms.CommandText = "SELECT * from Forms where userId = @userId";

            DbParameter paramId = cmdAllForms.CreateParameter();
            paramId.ParameterName = "userId";
            paramId.DbType = DbType.Int32;
            paramId.Value = userId;

            cmdAllForms.Parameters.Add(paramId);
            using (DbDataReader reader = cmdAllForms.ExecuteReader()) {
                while (reader.Read()) {
                    forms.Add(new Form {
                        FormId= Convert.ToInt32(reader["formId"]),
                        UserId = Convert.ToInt32(reader["userId"]),
                        FormName = Convert.ToString(reader["formName"]),
                        CreateDate = Convert.ToDateTime(reader["createDate"])
                    });
                }
            }
            return forms;
        }

        public List<int> GetQuestions(int formId) {
            throw new NotImplementedException();
        }

        public bool Insert(Form form) {
            if ((this._conn == null) || (this._conn.State != ConnectionState.Open)) {
                return false;
            }
            DbCommand cmdInsert = this._conn.CreateCommand();

            cmdInsert.CommandText = "INSERT into forms value(null, @userId, @formName, @createDate)";

            DbParameter paramId = cmdInsert.CreateParameter();
            paramId.ParameterName = "userId";
            paramId.DbType = DbType.Int32;
            paramId.Value = form.UserId;

            DbParameter paramName = cmdInsert.CreateParameter();
            paramName.ParameterName = "formName";
            paramName.DbType = DbType.String;
            paramName.Value = form.FormName;

            DbParameter paramDate = cmdInsert.CreateParameter();
            paramDate.ParameterName = "createDate";
            paramDate.DbType = DbType.Date;
            paramDate.Value = form.CreateDate;


            cmdInsert.Parameters.Add(paramId);
            cmdInsert.Parameters.Add(paramName);
            cmdInsert.Parameters.Add(paramDate);

            Debug.WriteLine(form.toString());

            return cmdInsert.ExecuteNonQuery() == 1;
        }
    }
}
