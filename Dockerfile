FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/Haru.Api/Haru.Api.csproj", "src/Haru.Api/"]
RUN dotnet restore "src/Haru.Api/Haru.Api.csproj"
COPY . .
WORKDIR "/src/src/Haru.Api"
RUN dotnet build "Haru.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Haru.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
CMD ASPNETCORE_URLS=http://*:$PORT dotnet Haru.Api..dll
