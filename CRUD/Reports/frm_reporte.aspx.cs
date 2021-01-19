﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using CrystalDecisions.CrystalReports.Engine;
using System.Configuration;
using CrystalDecisions.Reporting;
using CrystalDecisions.Shared;
using System.IO;
using CRUD.Models;

namespace CRUD.Reports
{
    public partial class frm_reporte : System.Web.UI.Page
    {
        consultasSQL dataSQL = new consultasSQL();
        ReportDocument rprt = new ReportDocument();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int tipo = Convert.ToInt32(Request.QueryString["tipo"]);
                switch (tipo)
                {
                    case 1:
                        Reporte();
                        break;
                }
            }
        }

        private void Reporte()
        {

            ReportDocument crystalReport = new ReportDocument();
            crystalReport.Load(Server.MapPath("/Reports/PersonInfo.rpt"));
            bdPersonal dsPersona = ReadAll();
            crystalReport.SetDataSource(dsPersona);
            CrystalReportViewer1.ReportSource = crystalReport;
            CrystalReportViewer1.Visible = true;
        }

        public bdPersonal ReadAll()
        {
            string conString = ConfigurationManager.ConnectionStrings["EmpleadosEntities2"].ConnectionString;
            SqlCommand cmd = new SqlCommand("sp_CRUD");
            using (SqlConnection con = new SqlConnection(conString))
            {
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Action", "READ");
                    sda.SelectCommand = cmd;
                    using (bdPersonal dsCustomers = new bdPersonal())
                    {
                        sda.Fill(dsCustomers, "Personal");
                        return dsCustomers;
                    }
                }
            }
        }

    }
}