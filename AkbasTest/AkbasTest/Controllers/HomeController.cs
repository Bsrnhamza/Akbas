using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AkbasTest.Models;
using System.Diagnostics;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace AkbasTest.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            SqlConnection conn = new SqlConnection("Data Source=AKBSRV5;Initial Catalog=StajTest;Persist Security Info=True;User ID=Hamza;Password=Hmz2024**");
            SqlCommand cmd = new SqlCommand("SELECT TableId, FullName, Phone, Address FROM TableTest;",conn);

            List<TableTest> Test = new List<TableTest>();
            conn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                Test.Add(
                    new TableTest
                    {
                        TableId= (int)dr["TableId"],
                        FullName = (string)dr["FullName"],
                        Address = (string)dr["Address"],
                        Phone = (string)dr["Phone"],
                    }
                    );
            }
            conn.Close();
            return View(Test);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
