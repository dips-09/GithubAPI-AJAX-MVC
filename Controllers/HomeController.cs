using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Homework7.Models;
using Microsoft.Extensions.Configuration;

namespace Homework7.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _config;
        private readonly static string _User = "fabpot";

        public HomeController(ILogger<HomeController> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetUserRepositories()
        {
            string url = "https://api.github.com/users/" + _User + "/repos";
            GithubRepo rep = new GithubRepo(url);
            List<UserRepository> repos = rep.GetAllRepositories().ToList();
            return Json(repos);
        }

        [HttpGet]
        public IActionResult GetUser()
        {
            string url = "https://api.github.com/users/" + _User + "";
            GithubRepo rep = new GithubRepo(url);
            UserInfo info = rep.GetUserInfo();
            return Json(info);
        }


        [HttpGet]
        [Route("api/commits/{repo}")]
        public IActionResult commits(string repo)
        {
            string url = "https://api.github.com/repos/"+ _User +"/" + repo + "/commits";
            GithubRepo rep = new GithubRepo(url);
            List<Commit> info = rep.GetCommits("User");
            return Json(info);
        }

        public IActionResult Privacy()
        {
            string secret = _config["AJAX1:MySecretToken"];
            Debug.WriteLine("Secret " + secret);
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
