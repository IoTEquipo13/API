Karon API
=========

This is the API for the app and service called "Space it". 

Installation
============

The only way to install these API is by cloning the repo and compiling it.
These API uses Firebase to store the data, a file called appsettings.json must be created 
to get the URL and Secret from Firebase. the file should be written as follows.

```json
{
    Url:"<yourFirebaseUrl>",
    Secret:"<yourFirebaseSecret"
}
```

To be able to compile you also need to install ASP.NET Core 1.0, the runtime version is 1.0.0-rc1-update1
These version must be hosted on a Windos Server, it uses a Firesharp, which is only supported on the full .NET Framework.

