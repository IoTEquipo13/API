using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;

using KaronAPI.Models;
using KaronAPI.ViewModel;
using KaronAPI.Interfaces;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace KaronAPI.Controllers
{
    public class DeviceController : Controller
    {
        [FromServices]
        public IPlaceRepository placeMethod { get; set; }

        [HttpPost]
        public async Task<IActionResult> Update([FromBody] DeviceVM device)
        {
            var place = await  placeMethod.Get(device.DeviceId, device.PlaceId);
            place.Status = device.Status;
            if(await placeMethod.Update(place, device.DeviceId, device.PlaceId))
            {
                return Json(true);
            }
            else
            {
                return Json(false);
            }
        }
    }
}
