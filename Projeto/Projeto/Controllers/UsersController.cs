using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Projeto.Models;

namespace Projeto.Controllers
{
    public class UsersController : Controller 
    {
        [Route("Usuarios")]
        [Route("Usuarios/Inicio")]
        public IActionResult Index()
        {
            int? Result = HttpContext.Session.GetInt32("ResultSession");
            ViewBag.SessionVerify = HttpContext.Session.GetInt32("ResultSession");
            if (Result == 1)
            {
                User user = JsonConvert.DeserializeObject<User>(HttpContext.Session.GetString("UserSession"));
                ViewBag.UserName = user.Name;
                using (UserModel model = new UserModel())
                {
                    List<User> lista = model.Read();
                    return View(lista);
                }
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        [Route("Usuarios/Detalhes/{id}")]
        public IActionResult Details(int id)
        {
            int? Result = HttpContext.Session.GetInt32("ResultSession");
            ViewBag.SessionVerify = HttpContext.Session.GetInt32("ResultSession");
            if (Result == 1)
            {
                User user = JsonConvert.DeserializeObject<User>(HttpContext.Session.GetString("UserSession"));
                ViewBag.UserName = user.Name;
            }
            using (UserModel model = new UserModel())
            {
                User user = model.Search(id);
                return View(user);
            }
        }

        [Route("Usuarios/Criação")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost("Usuarios/Create")]
        [ValidateAntiForgeryToken]
        public IActionResult Create(IFormCollection form)
        {
            User user = new User();
            user.Name = form["Name"];
            user.Email = form["Email"];

            using (UserModel model = new UserModel())
            {
                user.Password = model.CriptografaSenha(form["Password"]);
                model.Create(user);
                return RedirectToAction("Login");
            }
        }

        [Route("Usuarios/Login")]
        public IActionResult Login()
        {
            int? Result = HttpContext.Session.GetInt32("ResultSession");
            ViewBag.SessionVerify = HttpContext.Session.GetInt32("ResultSession");
            if (Result != 1)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        [HttpPost("Usuarios/Login")]
        [ValidateAntiForgeryToken]
        public IActionResult Login(IFormCollection form)
        {
            int? Result = HttpContext.Session.GetInt32("ResultSession");
            ViewBag.SessionVerify = HttpContext.Session.GetInt32("ResultSession");
            if (Result == 1)
            {
                return RedirectToAction("Index");
            }
            else
            {
                User user = new User();
                user.Name = form["Name"];

                using (UserModel model = new UserModel())
                {
                    user.Password = model.CriptografaSenha(form["Password"]);
                    int result = model.Login(user);
                    if (result != 0)
                    {
                        User index = model.Search(result);
                        int resultInt = 1;
                        HttpContext.Session.SetString("UserSession", JsonConvert.SerializeObject(index));
                        HttpContext.Session.SetInt32("ResultSession", resultInt);
                        ViewBag.SessionVerify = HttpContext.Session.GetInt32("ResultSession");
                        return RedirectToAction("index");
                    }
                    else
                    {
                        ViewBag.Erro = "Usuário ou senha invalidos!";
                        return View();
                    }
                }
            }
        }

        [Route("Usuarios/Editar/{id}")]
        public IActionResult Edit(int id)
        {
            int? Result = HttpContext.Session.GetInt32("ResultSession");
            ViewBag.SessionVerify = HttpContext.Session.GetInt32("ResultSession");
            if (Result == 1)
            {
                User user = JsonConvert.DeserializeObject<User>(HttpContext.Session.GetString("UserSession"));
                ViewBag.UserName = user.Name;
                if (id == user.Id)
                {
                    using (UserModel model = new UserModel())
                    {
                        User search = model.Search(id);
                        return View(search);
                    }
                }
            }
            return RedirectToAction("Index");
        }

        [HttpPost("Usuarios/Editar/{id}")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(IFormCollection form, int id)
        {
            User user = new User();
            user.Name = form["Name"];
            user.Email = form["Email"];

            using (UserModel model = new UserModel())
            {
                model.Update(user, id);
                int resultInt = 0;
                HttpContext.Session.SetInt32("ResultSession", resultInt);
                return RedirectToAction("Login");
            }
        }

        // GET: Users/Delete/5
        [Route("Usuarios/Deletar/{id}")]
        public IActionResult Delete(int id)
        {
            int? Result = HttpContext.Session.GetInt32("ResultSession");
            ViewBag.SessionVerify = HttpContext.Session.GetInt32("ResultSession");
            if (Result == 1)
            {
                User user = JsonConvert.DeserializeObject<User>(HttpContext.Session.GetString("UserSession"));
                ViewBag.UserName = user.Name;
                if (id == user.Id)
                {
                    using (UserModel model = new UserModel())
                    {
                        model.Delete(id);
                        int resultInt = 0;
                        HttpContext.Session.SetInt32("ResultSession", resultInt);
                        return RedirectToAction("Login");
                    }
                }
            }
            return RedirectToAction("Index");
        }

        [Route("Usuarios/Logout")]
        public IActionResult Logout()
        {
            int resultInt = 0;
            string index = null;
            HttpContext.Session.SetString("UserSession", JsonConvert.SerializeObject(index));
            HttpContext.Session.SetInt32("ResultSession", resultInt);
            ViewBag.SessionVerify = HttpContext.Session.GetInt32("ResultSession");
            return RedirectToAction("Login");
        }
    }
}
