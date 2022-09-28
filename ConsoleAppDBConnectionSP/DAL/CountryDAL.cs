using ConsoleAppDBConnectionSP.Model;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppDBConnectionSP.DAL
{
    class CountryDAL
    {
        private string _connectionString;
        public CountryDAL(IConfiguration iconfiguration)
        {
            _connectionString = iconfiguration.GetConnectionString("SqlConnection");
        }
        public List<CountryModel> GetList()
        {
            var listCountryModel = new List<CountryModel>();
            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("SP_COUNTRY_GET_LIST", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        listCountryModel.Add(new CountryModel
                        {
                            Id = Convert.ToInt32(rdr[0]),
                            Country = rdr[1].ToString(),
                            Active = Convert.ToBoolean(rdr[2])
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listCountryModel;
        }
    }
}
