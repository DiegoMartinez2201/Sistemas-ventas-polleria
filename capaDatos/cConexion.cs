﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace capaDatos
{
    public class cConexion
    {
         #region singleton
        private static readonly cConexion UnicaInstancia = new cConexion(); 
        public static cConexion Instancia
        {
            get
            {
                return cConexion.UnicaInstancia;
            }
        }
        #endregion singleton
        #region metodos
        public SqlConnection Conectar()
        {
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = "Data Source=DESKTOP-ANB050P\\SQLEXPRESS;initial Catalog=DB_PolleriaAdv;" + "Integrated Security=true; Encrypt=False;";
            return cn;
        }
        #endregion metodos
    }
}
          