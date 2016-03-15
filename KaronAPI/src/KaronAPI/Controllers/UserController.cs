using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Http;
using System.IO;
using Microsoft.Net.Http.Headers;

using KaronAPI.Interfaces;
using KaronAPI.ViewModel;
using KaronAPI.Models;


// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace KaronAPI.Controllers
{
    public class UserController : Controller
    {
        [FromServices]
        public IUserRepository userMethods { get; set; }

        public Task<string> Test()
        {
            return Task.FromResult("OK!");
        }

        [HttpGet]
        public async Task<IActionResult> GetUser(string Id)
        {
            var userId = await userMethods.Search(Id);
            return Json(userId);
        }

        [HttpPost]
        public async Task<IActionResult> AddPlate(string id,[FromBody]string Plate)
        {
            var user = await userMethods.Get(id);

            user.Plate.Add(Plate);

            if(await userMethods.Update(user, id))
            {
                return Json(true);
            }
            else
            {
                return Json(false);
            }
        }
        
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] UserVM user)
        {
            if (ModelState.IsValid)
            {
                var prefs = new Dictionary<DayOfWeek, string>();
                var plate = new List<string>();
                plate.Add(user.Plate);

                foreach (var day in user.PrefSegment)
                {
                    prefs.Add((DayOfWeek)Enum.Parse(typeof(DayOfWeek), day.Key.ToString()), day.Value.ToString());
                }

                var newUser = new User
                {
                    Condition = user.Condition,
                    Mail = user.Mail,
                    Name = user.Name,
                    Photo = user.Photo,
                    Plate = plate,
                    PrefSegment = prefs
                };
                var id = await userMethods.Create(newUser);
                if (id != null)
                {
                    return Json(id);
                }
                else
                {
                    return Json("Sorry");
                }

            }
            else
            {
                return Json("Sorry");
            }
        }
    }
}
