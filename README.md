# Ecommerce


1. Ensure your connection strings in `appsettings.json` point to a local SQL Server instance.

2. Open a command prompt in the Web folder and execute the following commands:

```
dotnet restore
dotnet ef database update -c catalogcontext -p ../Infrastructure/Infrastructure.csproj -s Web.csproj
dotnet ef database update -c appidentitydbcontext -p ../Infrastructure/Infrastructure.csproj -s Web.csproj
```

These commands will create two separate databases, one for the store's catalog data and shopping cart information, and one for the app's user credentials and identity data.

3. Run the application.
The first time you run the application, it will seed both databases with data such that you should see products in the store, and you should be able to log in using the demouser@microsoft.com account.

Note: If you need to create migrations, you can use these commands:
```
-- create migration (from Web folder CLI)
dotnet ef migrations add InitialModel --context catalogcontext -p ../Infrastructure/Infrastructure.csproj -s Web.csproj -o Data/Migrations

dotnet ef migrations add InitialIdentityModel --context appidentitydbcontext -p ../Infrastructure/Infrastructure.csproj -s Web.csproj -o Identity/Migrations
```

## Running the sample using Docker

You can run both the Web and WebRazorPages samples at the same time by running these commands from the root folder (where the .sln file is located):

```
    docker-compose build
    docker-compose up
```

You should be able to make requests to localhost:5106 and localhost:5107 once these commands complete.

You can run just the Web or WebRazorPages application by using the instructions located in their respective `Dockerfile` files in the root of the projects. Again, run these commands from the root of the solution (where the .sln file is located).
