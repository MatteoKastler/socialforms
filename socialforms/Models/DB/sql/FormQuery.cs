﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace socialforms.Models.DB.sql
{
    public class FormQuery : IFormQuery {

        private string _connString = "Server=localhost;database=;user=root;password=MBigubb75#";
        DbConnection _conn;
        public int cntQuestions(int formId) {
            throw new NotImplementedException();
        }

        public int cntUseranswers() {
            throw new NotImplementedException();
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
                Form temp = new Form { 
                    FormId = Convert.ToInt32(reader["formId"]),
                    UserId = Convert.ToInt32(reader["userId"]),
                    FormName = Convert.ToString(reader["formName"]),
                    CreateDate = Convert.ToDateTime(reader["createDate"])
                };
                return temp;
            }
        }
    
        public List<int> GetQuestions(int formId) {
            throw new NotImplementedException();
        }

        public bool Insert(Form form) {
            if ((this._conn == null) || (this._conn.State != ConnectionState.Open)) {
                return false;
            }
            DbCommand cmdInsert = this._conn.CreateCommand();

            cmdInsert.CommandText = "INSERT into forms value(null, @userId, @formName, @createDate )";

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

            return cmdInsert.ExecuteNonQuery() == 1;
        }
    }
}