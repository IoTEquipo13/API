using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.OptionsModel;
using Newtonsoft.Json.Linq;

using FireSharp;
using FireSharp.Config;

using KaronAPI.Interfaces;
using KaronAPI.Services;
using KaronAPI.Models;

namespace KaronAPI.Repository
{
    public class UserRepository : IUserRepository
    {
        private FirebaseClient client;
        public UserRepository(IOptions<FirebaseService> fireOps)
        {
            client = new FirebaseClient(new FirebaseConfig
            {
                AuthSecret = fireOps.Value.Secret,
                BasePath = fireOps.Value.Url
            });
        }

        public async Task<bool> Update(User user, string Id)
        {
            var response = await client.UpdateAsync($"Users/{Id}", user);
            if(response.StatusCode.ToString() == "OK")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<string> Create(User user)
        {
            var response = await client.PushAsync("Users/", user);
			if(response.StatusCode.ToString() == "OK")
            {
                JObject id = response.ResultAs<JObject>();
                string name = id["name"].ToString();
                return name;
            }
            else
            {
                return "Sorry";
            }
        }

        public async Task<User> Get(string id)
        {
            var response = await client.GetAsync($"Users/{id}");
            var user = response.ResultAs<User>();
            return user;
        }

        public async Task<Dictionary<string, User>> Get()
        {
            var response = await client.GetAsync($"Users/");
            var user = response.ResultAs<Dictionary<string, User>>();
            return user;
        }

        public async Task<string> Search(string plate)
        {
            var users = await Get();
            string id = "none";

            Parallel.ForEach(users, user =>
            {
                Parallel.ForEach(user.Value.Plate, userPlate =>
                {
                    if (plate == userPlate)
                    {
                        id = user.Key;
                    }
                });
            });

            return id;
        }
    }
}
