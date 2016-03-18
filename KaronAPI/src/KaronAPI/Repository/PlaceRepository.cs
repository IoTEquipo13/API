using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.OptionsModel;

using FireSharp;
using FireSharp.Config;

using KaronAPI.Interfaces;
using KaronAPI.Models;
using KaronAPI.Services;

namespace KaronAPI.Repository
{
    public class PlaceRepository : IPlaceRepository
    {
        private FirebaseClient client;
        public PlaceRepository(IOptions<FirebaseService> firebaseOps)
        {
            client = new FirebaseClient(new FirebaseConfig
            {
                AuthSecret = firebaseOps.Value.Secret,
                BasePath = firebaseOps.Value.Url
            });
        }

        public async Task<bool> Update(Place place, string device, int placeId)
        {
            var response = await client.UpdateAsync($"Parkinglot/ITESMGDA/{device}/Places/{placeId}/", place);
            if (response.StatusCode.ToString() == "OK")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Task<List<string>> Search(User user)
        {
            var prefredPlace = new List<string>();

            Parallel.ForEach(user.PrefSegment, prefered =>
            {
                if (prefered.Key.Equals(DateTime.Now.DayOfWeek))
                {
                    prefredPlace.Add(prefered.Value);
                    prefredPlace.Add(user.Condition);
                }
            });

            return Task.FromResult(prefredPlace);
        }

        public async Task<int> Get(List<string> preferences)
        {
            var response = await client.GetAsync($"Parkinglot/ITESMGDA/{preferences[0]}/Places/");
            var segment = response.ResultAs<List<Place>>();
            int segmentId = 1000000000;
            Parallel.For(0, segment.Count, i =>
            {
                if ((segment[i].Status == 0) && (segment[i].Type == preferences[1]))
                {
                    segmentId = i;
                }
            });

            if(segmentId != 1000000000)
            {
                return segmentId;
            }
            else
            {
                return 1000000000;
            }
        }

        public async Task<Place> Get(string device, int place)
        {
            var response = await client.GetAsync($"Parkinglot/ITESMGDA/{device}/Places/{place}/");
            var space = response.ResultAs<Place>();
            return space;
        }

        public async Task<bool> Save(string zone, int place)
        {
            var place2 = await Get(zone, place);
            place2.Status = 2;
            if(await Update(place2, zone, place))
            {
                return true;
            }
            return false;
        }

    }
}
