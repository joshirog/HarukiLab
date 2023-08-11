## Migrations
~~~
* dotnet ef database update -p src/Haru.Api
* dotnet ef database update 0 -p src/Haru.Api
* dotnet ef migrations remove -p src/Haru.Api
* dotnet ef migrations add CreateInitialScheme -p src/Haru.Api -o Persistences/Migrations
~~~

## References
~~~
* https://www.c-sharpcorner.com/article/jwt-authentication-with-refresh-tokens-in-net-6-0/
~~~