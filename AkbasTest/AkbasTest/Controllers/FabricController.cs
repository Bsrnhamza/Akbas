using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using AkbasTest.Models;
using Microsoft.AspNetCore.Mvc;

namespace AkbasTest.Controllers
{
    public class FabricController : Controller
    {
        private readonly string _connectionString = "Server=AKBSRV5;Database=StajTest;User Id=Hamza;Password=Hmz2024**;";

        // Index action: Tüm kumaşları listeler
        public IActionResult Index()
        {
            List<Fabric> fabrics = new List<Fabric>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "SELECT FabricId, FabricName, FabricType, Color, Price FROM Fabrics;";
                SqlCommand cmd = new SqlCommand(query, conn);

                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    fabrics.Add(new Fabric
                    {
                        FabricId = (int)dr["FabricId"],
                        FabricName = dr["FabricName"].ToString(),
                        FabricType = dr["FabricType"].ToString(),
                        Color = dr["Color"].ToString(),
                        Price = (decimal)dr["Price"]
                    });
                }
            }

            return View(fabrics);
        }

        // Create action: Yeni bir kumaş ekler
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Fabric model)
        {
            if (ModelState.IsValid)
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    string query = "INSERT INTO Fabrics (FabricName, FabricType, Color, Price) VALUES (@FabricName, @FabricType, @Color, @Price);";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@FabricName", model.FabricName);
                    cmd.Parameters.AddWithValue("@FabricType", model.FabricType);
                    cmd.Parameters.AddWithValue("@Color", model.Color);
                    cmd.Parameters.AddWithValue("@Price", model.Price);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }

                return RedirectToAction("Index");
            }

            return View(model);
        }

        // Edit action: Varolan bir kumaşı düzenler
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Fabric model;

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "SELECT FabricId, FabricName, FabricType, Color, Price FROM Fabrics WHERE FabricId = @Id;";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", id);

                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    model = new Fabric
                    {
                        FabricId = (int)dr["FabricId"],
                        FabricName = dr["FabricName"].ToString(),
                        FabricType = dr["FabricType"].ToString(),
                        Color = dr["Color"].ToString(),
                        Price = (decimal)dr["Price"]
                    };
                }
                else
                {
                    return NotFound();
                }
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Fabric model)
        {
            if (id != model.FabricId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    string query = "UPDATE Fabrics SET FabricName = @FabricName, FabricType = @FabricType, Color = @Color, Price = @Price WHERE FabricId = @Id;";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@FabricName", model.FabricName);
                    cmd.Parameters.AddWithValue("@FabricType", model.FabricType);
                    cmd.Parameters.AddWithValue("@Color", model.Color);
                    cmd.Parameters.AddWithValue("@Price", model.Price);
                    cmd.Parameters.AddWithValue("@Id", model.FabricId);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }

                return RedirectToAction("Index");
            }

            return View(model);
        }

        // Delete action: Varolan bir kumaşı siler
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "DELETE FROM Fabrics WHERE FabricId = @Id;";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", id);

                conn.Open();
                cmd.ExecuteNonQuery();
            }

            return RedirectToAction("Index");
        }
    }
}
