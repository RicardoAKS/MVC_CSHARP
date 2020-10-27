using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Projeto.Models;

namespace Projeto.Controllers
{
    public class UsersController : Controller
    {

        // GET: Users
        [Route("Usuarios")]
        [Route("Users")]
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
        public IActionResult Create(Microsoft.AspNetCore.Http.IFormCollection form)
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

        // GET: Users/Edit/5
        /*[Route("Usuarios/Editar/{id}")]
        [Route("Users/Edit/{id}")]*/

        // GET: Users/Delete/5
        /*[Route("Usuarios/Deletar/{id}")]
        [Route("Users/Delete/{id}")]*/
    }
}
