using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CRUD.Models
{
    public class consultasSQL
    {
        public static string conBD = ConfigurationManager.ConnectionStrings["EmpleadosEntities2"].ConnectionString;
        SqlConnection con = new SqlConnection(conBD);
        SqlCommand comm = new SqlCommand();

        public bool Create(Personal personal)
        {
            int res;
            bool i = false;
            con.Open();
            comm.Connection = con;
            comm.CommandType = System.Data.CommandType.StoredProcedure;
            comm.CommandText = "sp_CRUD";
            comm.Parameters.Clear();
            comm.Parameters.AddWithValue("@Action", "INSERT");
            comm.Parameters.AddWithValue("@Nombre", personal.Nombre);
            comm.Parameters.AddWithValue("@ApePaterno", personal.ApePaterno);
            comm.Parameters.AddWithValue("@ApeMaterno", personal.ApeMaterno);
            comm.Parameters.AddWithValue("@Edad", personal.Edad);
            comm.Parameters.AddWithValue("@IsActive", personal.IsActive);
            res = comm.ExecuteNonQuery();
            if (res == 1)
            {
                i = true;
            }
            con.Close();
            return i;
        }
        public List<Personal> ReadAll()
        {
            Personal ps = new Personal();
            List<Personal> lsPersonal = new List<Personal>();
            con.Open();
            comm.Connection = con;
            comm.CommandType = System.Data.CommandType.StoredProcedure;
            comm.CommandText = "sp_CRUD";
            comm.Parameters.AddWithValue("@Action", "READ");
            SqlDataReader reader = comm.ExecuteReader();
            while (reader.Read())
            {
                ps.ID_personal = Convert.ToInt32(reader["ID_personal"]);
                ps.Nombre = Convert.ToString(reader["Nombre"]);
                ps.ApePaterno = Convert.ToString(reader["ApePaterno"]);
                ps.ApeMaterno = Convert.ToString(reader["ApeMaterno"]);
                ps.Edad = Convert.ToInt32(reader["Edad"]);
                ps.IsActive = Convert.ToBoolean(reader["IsActive"]);
                lsPersonal.Add(new Personal()
                {
                    ID_personal = ps.ID_personal,
                    Nombre = ps.Nombre,
                    ApePaterno = ps.ApePaterno,
                    ApeMaterno = ps.ApeMaterno,
                    Edad = ps.Edad,
                    IsActive = ps.IsActive
                });
            }
            con.Close();
            return lsPersonal;
        }
        public bool Update(Personal personal)
        {
            int i;
            bool resp = false;
            con.Open();
            comm.Connection = con;
            comm.CommandType = System.Data.CommandType.StoredProcedure;
            comm.CommandText = "sp_CRUD";
            comm.Parameters.Clear();
            comm.Parameters.AddWithValue("@Action", "UPDATE");
            comm.Parameters.AddWithValue("@ID_personal", personal.ID_personal);
            comm.Parameters.AddWithValue("@Nombre", personal.Nombre);
            comm.Parameters.AddWithValue("@ApePaterno", personal.ApePaterno);
            comm.Parameters.AddWithValue("@ApeMaterno", personal.ApeMaterno);
            comm.Parameters.AddWithValue("@Edad", personal.Edad);
            comm.Parameters.AddWithValue("@IsActive", personal.IsActive);
            i = comm.ExecuteNonQuery();
            if (i == 1)
            {
                resp = true;
            }
            con.Close();
            return resp;
        }
        public bool Delete(int ID_personal)
        {
            int i;
            bool resp = false;
            con.Open();
            comm.Connection = con;
            comm.CommandType = System.Data.CommandType.StoredProcedure;
            comm.CommandText = "sp_CRUD";
            comm.Parameters.Clear();
            comm.Parameters.AddWithValue("@Action", "DELETE");
            comm.Parameters.AddWithValue("@ID_personal", ID_personal);
            i = comm.ExecuteNonQuery();
            if (i == 1)
            {
                resp = true;
            }
            con.Close();
            return resp;
        }

        public List<Personal> Details(int ID_personal)
        {
            Personal ps = new Personal();
            List<Personal> lsPersonal = new List<Personal>();
            con.Open();
            comm.Connection = con;
            comm.CommandType = System.Data.CommandType.StoredProcedure;
            comm.CommandText = "sp_CRUD";
            comm.Parameters.Clear();
            comm.Parameters.AddWithValue("@Action", "DETAILS");
            comm.Parameters.AddWithValue("@Id_personal", ID_personal);
            SqlDataReader reader = comm.ExecuteReader();
            while (reader.Read())
            {
                ps.ID_personal = Convert.ToInt32(reader["ID_personal"]);
                ps.Nombre = Convert.ToString(reader["Nombre"]);
                ps.ApePaterno = Convert.ToString(reader["ApePaterno"]);
                ps.ApeMaterno = Convert.ToString(reader["ApeMaterno"]);
                ps.Edad = Convert.ToInt32(reader["Edad"]);
                ps.IsActive = Convert.ToBoolean(reader["IsActive"]);
                lsPersonal.Add(new Personal()
                {
                    ID_personal = ps.ID_personal,
                    Nombre = ps.Nombre,
                    ApePaterno = ps.ApePaterno,
                    ApeMaterno = ps.ApeMaterno,
                    Edad = ps.Edad,
                    IsActive = ps.IsActive
                });
            }
            con.Close();
            return lsPersonal;
        }
    }
}