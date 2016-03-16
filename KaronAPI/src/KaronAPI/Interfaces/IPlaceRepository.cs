using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using KaronAPI.Models;

namespace KaronAPI.Interfaces
{
    public interface IPlaceRepository
    {
        Task<List<string>> Search(User user);
        Task<int> Get(List<string> preferences);
        Task<Place> Get(string device, int place);
        Task<bool> Update(Place place, string device, int placeId);
    }
}
