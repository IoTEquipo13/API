using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.NotificationHubs;

namespace KaronAPI.Models
{
    public class Notifications
    {
        public static Notifications Instance = new Notifications();

        public NotificationHubClient Hub { get; set; }

        private Notifications()
        {
            Hub = NotificationHubClient
                .CreateClientFromConnectionString("<yourEndpoint>","<yourHubName>");
        }
    }
}
