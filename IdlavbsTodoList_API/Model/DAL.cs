using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace IdlavbsTodoList_API
{
    public class DAL
    {
        string _connection = "Password=s1234L;Persist Security Info=True;User ID=sa;Initial Catalog=idlavbsTodoList;Data Source=MELISA";

        public Users checkLogin(string _userMail, string _userPassword)
        {
            SqlConnection conn = new SqlConnection(_connection);
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();

            cmd.CommandText = "SELECT * FROM Users Where UserMail='" + _userMail + "' and UserPassword='" + _userPassword + "'";

            SqlDataReader _rd = cmd.ExecuteReader();
            Users _user = new Users();
            while (_rd.Read())
            {
                if (_rd["UserID"] != DBNull.Value)
                {
                    _user.UserID = Convert.ToInt32(_rd["UserID"].ToString());
                }

                if (_rd["UserName"] != DBNull.Value)
                {
                    _user.UserName = (_rd["UserName"].ToString());
                }

            }
            conn.Close();

            return _user;
        }

        public bool updateLastLogin(string _userid)
        {
            SqlConnection conn = new SqlConnection(_connection);
            SqlDataAdapter da = new SqlDataAdapter();
            SqlCommand _cmd = conn.CreateCommand();
            _cmd.CommandText = "UPDATE USERS SET LastLogin=GETDATE() WHERE UserID=" + _userid + "";
            da.SelectCommand = _cmd;
            conn.Open();

            int _gelenetkilenensatir = _cmd.ExecuteNonQuery();
            bool _bldurum = false;
            if (_gelenetkilenensatir != -1)
            {

                _bldurum = true;
            }
            conn.Close();
            return _bldurum;
        }

        public List<Work> listWork(string _userID)
        {
            SqlConnection conn = new SqlConnection(_connection);
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();

            cmd.CommandText = "SELECT WorkID,T0.UserID,WorkName,WorkDetail,WorkStatus,T0.CreateDate,FinishDate,UserName FROM WORKS T0 INNER JOIN USERS T1 ON T0.UserID=T1.UserID WHERE T0.UserID="+ _userID + "";

            SqlDataReader _rd = cmd.ExecuteReader();
            List<Work> _work = new List<Work>();
            while (_rd.Read())
            {
                Work _wr = new Work();
                if (_rd["UserID"] != DBNull.Value)
                {
                    _wr.UserID = Convert.ToInt32(_rd["UserID"].ToString());
                }
                if (_rd["WorkID"] != DBNull.Value)
                {
                    _wr.WorkID = Convert.ToInt32(_rd["WorkID"].ToString());
                }
                if (_rd["WorkName"] != DBNull.Value)
                {
                    _wr.WorkName = (_rd["WorkName"].ToString());
                }
                if (_rd["WorkDetail"] != DBNull.Value)
                {
                    _wr.WorkDetail = (_rd["WorkDetail"].ToString());
                }
                if (_rd["WorkStatus"] != DBNull.Value)
                {
                    _wr.WorkStatus = Convert.ToInt32(_rd["WorkStatus"].ToString());
                }
                if (_rd["CreateDate"] != DBNull.Value)
                {
                    _wr.CreateDate = Convert.ToDateTime(_rd["CreateDate"].ToString());
                }
                if (_rd["FinishDate"] != DBNull.Value)
                {
                    _wr.FinishDate = Convert.ToDateTime(_rd["FinishDate"].ToString());
                }
                if (_rd["UserName"] != DBNull.Value)
                {
                    _wr.UserName = (_rd["UserName"].ToString());
                }

                _wr.WorkStatusName(_wr.WorkStatus);

                _work.Add(_wr);
            }
            conn.Close();

            return _work;
        }

        public bool postWork(Work _work)
        {
            SqlConnection _cnn = new SqlConnection(_connection);
            _cnn.Open();
            SqlCommand _cmd = _cnn.CreateCommand();
            _cmd.CommandText = "insertWORK";
            _cmd.CommandType = System.Data.CommandType.StoredProcedure;
            _cmd.Parameters.AddWithValue("@WorkID", _work.WorkID);
            _cmd.Parameters.AddWithValue("@UserID", _work.UserID);
            _cmd.Parameters.AddWithValue("@WorkName", _work.WorkName);

            int _gelenetkilenensatir = _cmd.ExecuteNonQuery();
            bool _bldurum = false;
            if (_gelenetkilenensatir != -1)
            {

                _bldurum = true;
            }
            _cnn.Close();
            return _bldurum;

        }

        public bool deletWork(int _workid)
        {
            SqlConnection conn = new SqlConnection(_connection);
            SqlDataAdapter da = new SqlDataAdapter();
            SqlCommand _cmd = conn.CreateCommand();
            _cmd.CommandText = "DELETE FROM WORKS WHERE WorkID="+_workid+"";
            da.SelectCommand = _cmd;
            conn.Open();

            int _gelenetkilenensatir = _cmd.ExecuteNonQuery();
            bool _bldurum = false;
            if (_gelenetkilenensatir != -1)
            {

                _bldurum = true;
            }
            conn.Close();
            return _bldurum;
        }

    }

}
