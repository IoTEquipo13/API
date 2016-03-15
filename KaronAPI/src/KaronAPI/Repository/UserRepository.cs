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

		public Task<string> Create(User user)
        {
            var response = client.Push("Users/", user);
			if(response.StatusCode.ToString() == "OK")
            {
                JObject id = response.ResultAs<JObject>();
                string name = id["name"].ToString();
                return Task.FromResult(name);
            }
            else
            {
                return Task.FromResult("Sorry");
            }
        }
    }
}
