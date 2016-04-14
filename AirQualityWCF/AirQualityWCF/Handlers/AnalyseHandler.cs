using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirQualityWCF.Model;
using AirQualityWCF.Model.Helpers;

namespace AirQualityWCF.Handlers
{
    public class AnalyseHandler
    {
        private Connection _connection;
        private string error;
        private List<Analyse> _analyser = new List<Analyse>();

        public AnalyseHandler(Connection connection)
        {
            _connection = connection;
        }

        public List<Analyse> AnalyserByMonth(int month, int maxRows, int enhedId, int stofId, int udstyrId, int opstillingId)
        {
            _analyser.Clear();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = _connection.SqlConnection;
            string query = "SELECT";

            // Henter kun maxRows antal rækker hvis maxRows ikke er sat til 0
            if (maxRows != 0)
            {
                query += " Top (@maxRows)";
                cmd.Parameters.AddWithValue("@maxRows", maxRows);
            }
            query += " * FROM AmbientView WHERE Month(Datomaerke) = @month";
            cmd.Parameters.AddWithValue("@month", month);

            if (enhedId != 0)
            {
                query += " AND EnhedId = @EnhedId";
                cmd.Parameters.AddWithValue("@EnhedId", enhedId);
            }

            if (stofId != 0)
            {
                query += " AND StofId = @StofId";
                cmd.Parameters.AddWithValue("@StofId", stofId);
            }

            if (udstyrId != 0)
            {
                query += " AND UdstyrId = @UdstyrId";
                cmd.Parameters.AddWithValue("@UdstyrId", udstyrId);
            }

            if (opstillingId != 0)
            {
                query += " AND OpstillingId = @OpstillingId";
                cmd.Parameters.AddWithValue("@OpstillingId", opstillingId);
            }

            cmd.CommandText = query;

            FillListFromReader(cmd, 1);

            return _analyser;
        }

        private void FillListFromReader(SqlCommand cmd, int mode)
        {
            try
            {
                _connection.SqlConnection.Open();
                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        Analyse a = new Analyse();
                        Maalested m = new Maalested();
                        Opstilling o = new Opstilling();
                        Enhed e = new Enhed();
                        Udstyr u = new Udstyr();
                        Stof s = new Stof();

                        int easting32 = rdr.GetOrdinal("Easting_32");
                        m.Easting_32 = rdr.GetString(easting32);
                        int northing32 = rdr.GetOrdinal("Northing_32");
                        m.Northing_32 = rdr.GetString(northing32);
                        int mId = rdr.GetOrdinal("MaalestedId");
                        m.Id = rdr.GetInt32(mId);
                        int mNavn = rdr.GetOrdinal("MaalestedNavn");
                        m.Navn = rdr.GetString(mNavn);

                        o.Maalested = m;
                        int oId = rdr.GetOrdinal("OpstillingId");
                        o.Id = rdr.GetInt32(oId);
                        int oNavn = rdr.GetOrdinal("OpstillingNavn");
                        o.Navn = rdr.GetString(oNavn);

                        int eId = rdr.GetOrdinal("EnhedId");
                        e.Id = rdr.GetInt32(eId);
                        int eNavn = rdr.GetOrdinal("EnhedNavn");
                        e.Navn = rdr.GetString(eNavn);

                        int uId = rdr.GetOrdinal("UdstyrId");
                        u.Id = rdr.GetInt32(uId);
                        int uNavn = rdr.GetOrdinal("UdstyrNavn");
                        u.Navn = rdr.GetString(uNavn);

                        int sId = rdr.GetOrdinal("StofId");
                        s.Id = rdr.GetInt32(sId);
                        int sNavn = rdr.GetOrdinal("StofNavn");
                        s.Navn = rdr.GetString(sNavn);

                        a.Opstilling = o;
                        a.Enhed = e;
                        a.Udstyr = u;
                        a.Stof = s;
                        //int aId = rdr.GetOrdinal("AnalyseId");
                        //a.Id = rdr.GetInt32(aId);
                        int aRes = rdr.GetOrdinal("Resultat");
                        if (mode == 1)
                        {
                            a.Resultat = rdr.GetString(aRes);
                        }
                        else if (mode == 2)
                        {
                            a.Resultat = rdr.GetDecimal(aRes).ToString();
                        }
                        
                        int aDato = rdr.GetOrdinal("Datomaerke");
                        a.Datomaerke = rdr.GetDateTime(aDato);

                        _analyser.Add(a);
                    }
                }
            }
            catch (Exception ex)
            {
                error = ex.Message;
            }
        }

        public List<Analyse> GetAnalysisByMonth(int month, int stofId)
        {
            _analyser.Clear();
            SqlCommand cmd = new SqlCommand("GetAverageByMonthAndStof", _connection.SqlConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("month", month);
            cmd.Parameters.AddWithValue("StofId", stofId);

            try
            {
                FillListFromReader(cmd, 2);
            }
            catch (Exception ex)
            {
                error = ex.Message;
            }

            return _analyser;
        }

        public List<Stof> GetAllStof()
        {
            List<Stof> stoffer = new List<Stof>();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = _connection.SqlConnection;
            string query = "Select * from StofSet";
            cmd.CommandText = query;

            try
            {
                _connection.SqlConnection.Open();
                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        Stof s = new Stof();

                        int sId = rdr.GetOrdinal("Id");
                        int sNavn = rdr.GetOrdinal("Navn");
                        s.Id = rdr.GetInt32(sId);
                        s.Navn = rdr.GetString(sNavn);
                        stoffer.Add(s);
                    }
                }
            }
            catch (Exception ex)
            {
                error = ex.Message;
            }

            return stoffer;
        }

        public List<Udstyr> GetAllUdstyr()
        {
            List<Udstyr> udstyr = new List<Udstyr>();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = _connection.SqlConnection;
            string query = "Select * from UdstyrSet";
            cmd.CommandText = query;

            try
            {
                _connection.SqlConnection.Open();
                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        Udstyr u = new Udstyr();

                        int sId = rdr.GetOrdinal("Id");
                        int sNavn = rdr.GetOrdinal("Navn");
                        u.Id = rdr.GetInt32(sId);
                        u.Navn = rdr.GetString(sNavn);
                        udstyr.Add(u);
                    }
                }
            }
            catch (Exception ex)
            {
                error = ex.Message;
            }

            return udstyr;
        }

        public List<Maalested> GetAllMaalested()
        {
            List<Maalested> maal = new List<Maalested>();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = _connection.SqlConnection;
            string query = "Select * from MaalestedSet";
            cmd.CommandText = query;

            try
            {
                _connection.SqlConnection.Open();
                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        Maalested s = new Maalested();

                        int sId = rdr.GetOrdinal("Id");
                        int sNavn = rdr.GetOrdinal("Navn");
                        int sEas = rdr.GetOrdinal("Easting_32");
                        int sNor = rdr.GetOrdinal("Northing_32");
                        s.Id = rdr.GetInt32(sId);
                        s.Navn = rdr.GetString(sNavn);
                        s.Easting_32 = rdr.GetString(sEas);
                        s.Northing_32 = rdr.GetString(sNor);
                        maal.Add(s);
                    }
                }
            }
            catch (Exception ex)
            {
                error = ex.Message;
            }

            return maal;
        }

        public List<Opstilling> GetAllOpstilling()
        {
            List<Opstilling> ops = new List<Opstilling>();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = _connection.SqlConnection;
            string query = "Select * from OpstillingSet";
            cmd.CommandText = query;

            try
            {
                _connection.SqlConnection.Open();
                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        Opstilling s = new Opstilling();
                        Maalested m = new Maalested();

                        int sId = rdr.GetOrdinal("Id");
                        int sNavn = rdr.GetOrdinal("Navn");
                        int sMId = rdr.GetOrdinal("MaalestedId");
                        s.Id = rdr.GetInt32(sId);
                        s.Navn = rdr.GetString(sNavn);
                        m.Id = rdr.GetInt32(sMId);
                        s.Maalested = m;
                        ops.Add(s);
                    }
                }
            }
            catch (Exception ex)
            {
                error = ex.Message;
            }

            return ops;
        }

        public List<Enhed> GetAllEnhed()
        {
            List<Enhed> enheder = new List<Enhed>();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = _connection.SqlConnection;
            string query = "Select * from EnhedSet";
            cmd.CommandText = query;

            try
            {
                _connection.SqlConnection.Open();
                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        Enhed s = new Enhed();

                        int sId = rdr.GetOrdinal("Id");
                        int sNavn = rdr.GetOrdinal("Navn");
                        s.Id = rdr.GetInt32(sId);
                        s.Navn = rdr.GetString(sNavn);
                        enheder.Add(s);
                    }
                }
            }
            catch (Exception ex)
            {
                error = ex.Message;
            }

            return enheder;
        }
    }
}
