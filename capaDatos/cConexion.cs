using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace capaDatos
{
    class cConexionUnica
    {
        /*#region singleton
        private static readonly cConexionUnica UnicaInstancia = new cConexionUnica();
        public static cConexionUnica Instancia
        {
            get
            {
                return cConexionUnica.UnicaInstancia1;
            }
        }

        internal static cConexionUnica UnicaInstancia1 => UnicaInstancia;
        #endregion singleton
        #region metodos
        public SqlConnection Conectar()
        {
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = "Data Source=DESKTOP-HNG11PV\\SQLEXPRESS;initial Catalog=DB_Polleria;" + "Integrated Security=true";
            return cn;
        }
        #endregion metodos*/
    }
}
          