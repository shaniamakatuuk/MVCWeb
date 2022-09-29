using Microsoft.AspNetCore.Mvc;
using MVC_SIBKMNET.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_SIBKMNET.Controllers
{
    public class CountryController : Controller
    {

        SqlConnection sqlConnection;

        /*
         * Data Source ->
         * Initial Catalog ->
         * User Id -> ussername
         * Password -> password
         * connect Timeout
         */

        string connectionString = "Data Source=DESKTOP-CHRT4VD;Initial Catalog=SIBKMNET;" +
           "User ID=sibkmnet;Password=1234567890;Connect Timeout=30;";

        public object Id { get; private set; }

        // GET ALL
        // GET

        public IActionResult Index()
        {
            string query = "SELECT *FROM Country";

            sqlConnection = new SqlConnection(connectionString);
            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            List<Country> Countries = new List<Country>();

            try
            {
                sqlConnection.Open();
                using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                {
                    if (sqlDataReader.HasRows)
                    {
                        while (sqlDataReader.Read())
                        {
                            Country country = new Country();
                            country.Id = Convert.ToInt32(sqlDataReader[0]);
                            country.Name = sqlDataReader[1].ToString();
                            Countries.Add(country);
                        }
                    }
                    else
                    {
                        Console.WriteLine("No Data Rows");
                    }
                    sqlDataReader.Close();
                }
                sqlConnection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException);
            }
            return View(Countries);
        }



        // GET BY ID
        // GET

        public IActionResult Details(Country country)
        {
            string query = "SELECT * FROM Country WHERE Id = @id";

            SqlParameter sqlParameter = new SqlParameter();
            sqlParameter.ParameterName = "@id";
            sqlParameter.Value = Id;

            sqlConnection = new SqlConnection(connectionString);
            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            sqlCommand.Parameters.Add(sqlParameter);
            try
            {
                sqlConnection.Open();
                using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                {
                    if (sqlDataReader.HasRows)
                    {
                        while (sqlDataReader.Read())
                        {
                            Console.WriteLine(sqlDataReader[0] + "-" + sqlDataReader[1]);
                        }
                    }
                    else
                    {
                        Console.WriteLine("No Data Rows");
                    }
                    sqlDataReader.Close();
                }
                sqlConnection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException);
            }
            return View();
        }




        // CREAT
        // GEAT

        public IActionResult Creat()
        {
            return View();
        }
        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Country country)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                SqlTransaction sqlTransaction = sqlConnection.BeginTransaction();

                SqlCommand sqlCommand = sqlConnection.CreateCommand();
                sqlCommand.Transaction = sqlTransaction;

                SqlParameter sqlParameter = new SqlParameter();
                SqlParameter sqlParameter1 = new SqlParameter();
                SqlParameter sqlParameter2 = new SqlParameter();
                sqlParameter.ParameterName = "@name";
                sqlParameter.Value = country.Name;
                sqlParameter1.ParameterName = "@pembaruan";
                sqlParameter1.Value = country.Edit;
                sqlParameter2.ParameterName = "@id";
                sqlParameter2.Value = country.Id;
                //Console.WriteLine(country.Pembaruan);
                //Console.WriteLine(country.Id);

                sqlCommand.Parameters.Add(sqlParameter);
                sqlCommand.Parameters.Add(sqlParameter1);
                sqlCommand.Parameters.Add(sqlParameter2);

                try
                {
                    sqlCommand.CommandText = "UPDATE Country SET name = @pembaruan " + "WHERE Id = @id";
                    sqlCommand.ExecuteNonQuery();
                    sqlTransaction.Commit();
                }
                catch (Exception ex)
                {

                    Console.WriteLine(ex.InnerException);
                }
                return View();
            }
        }

        // UPDATE
        // GEAT
        public IActionResult Edit()
        {
            return View();
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Edit (Country country)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                SqlTransaction sqlTransaction = sqlConnection.BeginTransaction();

                SqlCommand sqlCommand = sqlConnection.CreateCommand();
                sqlCommand.Transaction = sqlTransaction;

                SqlParameter sqlParameter = new SqlParameter();
                SqlParameter sqlParameter1 = new SqlParameter();
                SqlParameter sqlParameter2 = new SqlParameter();
                sqlParameter.ParameterName = "@name";
                sqlParameter.Value = country.Name;
                sqlParameter1.ParameterName = "@pembaruan";
                sqlParameter1.Value = country.Edit;
                sqlParameter2.ParameterName = "@id";
                sqlParameter2.Value = country.Id;
                //Console.WriteLine(country.Pembaruan);
                //Console.WriteLine(country.Id);

                sqlCommand.Parameters.Add(sqlParameter);
                sqlCommand.Parameters.Add(sqlParameter1);
                sqlCommand.Parameters.Add(sqlParameter2);

                try
                {
                    sqlCommand.CommandText = "UPDATE Country SET name = @pembaruan " + "WHERE Id = @id";
                    sqlCommand.ExecuteNonQuery();
                    sqlTransaction.Commit();
                }
                catch (Exception ex)
                {

                    Console.WriteLine(ex.InnerException);
                }
                return View();
            }
        }

        // DELET
        // GEAT
        public IActionResult Delete()
        {
            return View();
        }
        // POST

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Delete(Country country)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                SqlTransaction sqlTransaction = sqlConnection.BeginTransaction();

                SqlCommand sqlCommand = sqlConnection.CreateCommand();
                sqlCommand.Transaction = sqlTransaction;

                SqlParameter sqlParameter = new SqlParameter();
                sqlParameter.ParameterName = "@id";
                sqlParameter.Value = country.Id;

                sqlCommand.Parameters.Add(sqlParameter);

                try
                {
                    sqlCommand.CommandText = "DELETE Country " + "WHERE (Id) = (@id)";
                    sqlCommand.ExecuteNonQuery();
                    sqlTransaction.Commit();
                }
                catch (Exception ex)
                {

                    Console.WriteLine(ex.InnerException);
                }
                return View();
            }
        }
    }
}