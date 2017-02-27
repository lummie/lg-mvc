# lg-mvc
A simple MVC app in .net found in the iStore directory.

## Building iStore

1. Clone the repo
2. `cd` into the iStore directory
3. Run `dotnet restore` to download dependencies
4. Run `dotnet ef database update` to create an initial database.
5. Run `dotnet build` to build 
6. Run `dotnet run --server.urls http://0.0.0.0:5000` to run.

Web server will be running on port 5000.

-----
A simple Job system **library** in the Jobs directory

## Building Jobs
1. Clone the repo
2. `cd` into the /Jobs/src/JobsService directory
3. Run `dotnet restore` to download dependencies
4. Run `dotnet build` to build 
5. `cd` into the Jobs/test/JobsService.Tests directory
6. Run `dotnet restore` to download dependencies
7. Run `dotnet test` to run the tests

