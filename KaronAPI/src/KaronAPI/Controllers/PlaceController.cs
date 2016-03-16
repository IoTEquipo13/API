using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;

using KaronAPI.Interfaces;
using KaronAPI.Models;
using KaronAPI.ViewModel;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860
namespace KaronAPI.Controllers
{
    public class PlaceController : Controller
    {    
        [FromServices]
        public IPlaceRepository placesMethods { get; set; }

        [FromServices]
        public IUserRepository userMethods { get; set; }

        [HttpGet]
        public async Task<IActionResult> GetZone(string Id)
        {   
            var user = await userMethods.Search(Id);
            var preferedZone = await placesMethods.Search(user);
            var place = await placesMethods.Get(preferedZone);
            if(place != 1000000000)
            {
                if(await placesMethods.Save(preferedZone[0], place))
                {
                    return Json(preferedZone[0]);
                }
            }
            return Json("none!");
        } 
    }
}
