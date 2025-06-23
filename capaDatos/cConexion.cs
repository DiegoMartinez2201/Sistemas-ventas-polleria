using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
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
            cn.ConnectionString = "Data Source=NELSON\\SQLEXPRESS;initial Catalog=Polleria;" + "Integrated Security=true";
            return cn;
        }
        #endregion metodos
    }
}
          