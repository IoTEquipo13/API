Karon API
=========

This is the API for the app and service called "Space it". 

Installation
============

The only way to install these API is by cloning the repo and compiling it.
These API uses Firebase to store the data, a file called appsettings.json must be created 
to get the URL and Secret from Firebase. The file should be written as follows.

```json
{
    "Url":"<yourFirebaseUrl>",
    "Secret":"<yourFirebaseSecret>"
}
```

The API also manages the push notifications to various kind of devices, it is written using the Notification Hub service on 
Microsoft Azure. Inside the namespace KaronAPI.Models, in the class named Notifications.cs, inside the private constructor, the different strings
must be setted, the first one is the Notification Hub endpoint and the second one is the Notification Hub name, this service is totally free. 

It looks something like these.
```C#
namespace KaronAPI.Models
{
    public class Notifications
    {
        public static Notifications Instance = new Notifications();

        public NotificationHubClient Hub { get; set; }

        private Notifications()
        {
            Hub = NotificationHubClient
                .CreateClientFromConnectionString("<yourEndpoint>", "<yourHubName>");
        }
    }
}
```


To be able to compile you also need to install ASP.NET Core 1.0, the runtime version is 1.0.0-rc1-update1
These version must be hosted on a Windos Server, it uses the Firesharp library, which is only supported on the full .NET Framework.

It can be runned with the Kestrel web server but I recommend using IIS unless the console is needed for debbuging.

Use
====

This API have various endpoints, those will be published on a Google doc, this file has all the endpoints that the API has, also how 
the JSON should be written, some endpoints have a note, those are to specify diffent scenarios. 
[These is the Url to the Google Doc. ](https://docs.google.com/document/d/1iUiO_sLul3Ki-qZamP-f1FqXhNyQuRVdYHVw8L2SAzM/edit?usp=sharing)