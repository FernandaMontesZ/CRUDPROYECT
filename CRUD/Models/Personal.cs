
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data.SqlClient;

namespace CRUD.Models
{
    
    public partial class Personal
    {
        public int ID_personal { get; set; }

        [StringLength(50, ErrorMessage = "El Nombre no puede contener mas de 50 caracteres.")]
        [Required]
        public string Nombre { get; set; }

        [StringLength(50, ErrorMessage = "El Apellido Paterno no puede contener mas de 50 caracteres.")]
        [Display(Name = "Apellido Paterno")]
        [Required]
        public string ApePaterno { get; set; }

        [StringLength(50, ErrorMessage = "El Apellido Materno no puede contener mas de 50 caracteres.")]
        [Display(Name = "Apellido Materno")]
        [Required]
        public string ApeMaterno { get; set; }

        [Required]
        [Range(1,100, ErrorMessage ="Debe ser una Edad valida ")]
        public Nullable<int> Edad { get; set; }

        [Required]
        public Nullable<bool> IsActive { get; set; }
    }

    public class EmpleadosBD {
        public static string conBD = ConfigurationManager.ConnectionStrings["EmpleadosEntities2"].ConnectionString;
        SqlConnection con = new SqlConnection(conBD);
        SqlCommand comm = new SqlCommand();
        Personal ps = new Personal();
        List<Personal> lsPersonal = new List<Personal>();

        public int create(Personal personal)
        {
            int i;
            con.Open();
            comm.Connection = con;
            comm.CommandType = System.Data.CommandType.StoredProcedure;
            comm.CommandText = "sp_CRUD";
            comm.Parameters.AddWithValue("@Action", "INSERT");
            comm.Parameters.AddWithValue("@Nombre", personal.Nombre);
            comm.Parameters.AddWithValue("@ApePaterno", personal.ApePaterno);
            comm.Parameters.AddWithValue("@ApeMaterno", personal.ApeMaterno);
            comm.Parameters.AddWithValue("@Edad", personal.Edad);
            comm.Parameters.AddWithValue("@IsActive", personal.IsActive);
            i = comm.ExecuteNonQuery();
            con.Close();
            return i;
        }
        public List<Personal> RealAll()
        {
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
        public int Update(Personal personal)
        {
            int i;
            con.Open();
            comm.Connection = con;
            comm.CommandType = System.Data.CommandType.StoredProcedure;
            comm.CommandText = "sp_CRUD";
            comm.Parameters.AddWithValue("@Action", "UPDATE");
            comm.Parameters.AddWithValue("@ID_personal", personal.ID_personal);
            comm.Parameters.AddWithValue("@Nombre", personal.Nombre);
            comm.Parameters.AddWithValue("@ApePaterno", personal.ApePaterno);
            comm.Parameters.AddWithValue("@ApeMaterno", personal.ApeMaterno);
            comm.Parameters.AddWithValue("@Edad", personal.Edad);
            comm.Parameters.AddWithValue("@IsActive", personal.IsActive);
            i = comm.ExecuteNonQuery();
            con.Close();
            return i;
        }
        public int Delete(int ID_personal)
        {
            int i;
            con.Open();
            comm.Connection = con;
            comm.CommandType = System.Data.CommandType.StoredProcedure;
            comm.CommandText = "sp_CRUD";
            comm.Parameters.AddWithValue("@Action", "DELETE");
            comm.Parameters.AddWithValue("@Id_personal", ID_personal);
            i = comm.ExecuteNonQuery();
            con.Close();
            return i;
        }

    }
}
