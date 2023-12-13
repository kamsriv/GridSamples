﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using System.Diagnostics;
using System.Text.Json;
using VFSample.Models;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Negotiate;
using Microsoft.AspNetCore.Diagnostics;
using MyProgram;

namespace VFSample.Controllers
{
    //[Authorize(AuthenticationSchemes = NegotiateDefaults.AuthenticationScheme)]
    public class HomeController : Controller
    {
        public class ABC
        {
            public virtual void work()
            {
                System.Console.WriteLine($"I am in ABC - Work");
            }
        }
        public class DEF : ABC
        {
            public override void work()
            {
                base.work();
                System.Console.WriteLine($"I am in DEF - Work");
            }
        }
        public class GHI : DEF
        {
            public override void work()
            {
                base.work();
                System.Console.WriteLine($"I am in GHI - Work");
            }
        }
        public void Main1(string[] args)
        {
            ABC x = new GHI();
            x.work();
        }


        List<ABC> list = new List<ABC>() { new GHI() { }, new GHI(), new DEF()  };


        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        public IActionResult HTMLTable()
        {
            var fl = System.IO.File.ReadAllText($"data_geo.json");

            var myVal = list.FirstOrDefault(p => p.Equals(new ABC()));


            return View("IndexTable", JsonSerializer.Deserialize<GeoModalData>(fl));
        }
        public IActionResult DataTable()
        {
            
            //read json file and send that information to cshtml as modal.
            
            var fl = System.IO.File.ReadAllText($"data_geo.json");
            return View("IndexDataTbls", JsonSerializer.Deserialize<GeoModalData>(fl));
        }
        public IActionResult BsTable()
        {
            var fl = System.IO.File.ReadAllText($"data_geo.json");
            return View("IndexBsTable", JsonSerializer.Deserialize<GeoModalData>(fl));
        }
        public IActionResult GridJs()
        {
            return View("GridJs");
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            //throw new Exception("Exception occurred.. this will take to the error page");
           return BadRequest("hello this will not good");
            //return View();
        }

        public IActionResult MyAction()
        {
            using (SqlConnection con = new SqlConnection())
            {
                con.Open();
                using(SqlCommand cmd = con.CreateCommand()) {
                    cmd.CommandText = "insert into tbl_empty values(@param1, @param2)";
                    cmd.Parameters.AddWithValue("@param1", "hello");
                    cmd.Parameters.AddWithValue("@param2", "second");
                    cmd.ExecuteNonQuery();
                }
            }
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            var exc = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            var url = Context.App.Configuration["AppUrl"];
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier, ErrorMessage = exc?.Error?.Message });
        }
    }
}