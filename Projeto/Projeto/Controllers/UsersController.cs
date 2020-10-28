using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Projeto.Models;

namespace Projeto.Controllers
{
    public class UsersController : Controller 
    {

        // GET: Users
        [Route("Usuarios")]
        public IActionResult Index()
        {
            using (UserModel model = new UserModel())
            {
                List<User> lista = model.Read();
                return View(lista);
            }
        }

        // GET: Users/Details/5
        /*[Route("Usuarios/Detalhes/{id}")]
        [Route("Users/Details/{id}")]*/

        // GET: Users/Create
        [Route("Usuarios/Criação")]
        [Route("Users/Create")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("Users/Create")]
        [ValidateAntiForgeryToken]
        public IActionResult Create(IFormCollection form)
        {
            User user = new User();
            user.Name = form["Name"];
            user.Email = form["Email"];
            user.Password = form["Password"];

            using (UserModel model = new UserModel())
            {
                model.Create(user);
                return RedirectToAction("Index");
            }
        }

        [Route("Usuarios/Login")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost("Users/Login")]
        [ValidateAntiForgeryToken]
        public IActionResult Login(IFormCollection form)
        {
            User user = new User();
            user.Name = form["Name"];
            user.Password = form["Password"];

            using (UserModel model = new UserModel())
            {
                User user1 = model.Login(user);
                if(user1)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View();
                }
            }
        }


            // GET: Users/Edit/5
        [Route("Usuarios/Editar/{id}")]
        [Route("Users/Edit/{id}")]
        public IActionResult Edit(int id)
        {
            using (UserModel model = new UserModel())
            {
                User user = model.Search(id);
                return View(user);
            }
        }

        [HttpPost("Users/Edit/{id}")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(IFormCollection form, int id)
        {
            User user = new User();
            user.Name = form["Name"];
            user.Email = form["Email"];
            user.Password = form["Password"];

            using (UserModel model = new UserModel())
            {
                model.Update(user, id);
                return RedirectToAction("Index");
            }
        }

        [Route("Usuarios/Detalhes/{id}")]
        public IActionResult Details(int id)
        {

            using (UserModel model = new UserModel())
            {
                User user = model.Search(id);
                return View(user);
            }
        }

        // GET: Users/Delete/5
        [Route("Usuarios/Deletar/{id}")]
        [Route("Users/Delete/{id}")]
        public IActionResult Delete(int id)
        {
            using (UserModel model = new UserModel())
            {
                model.Delete(id);
                return RedirectToAction("Index");
            }
        }
    }
}
