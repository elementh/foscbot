FROM mcr.microsoft.com/dotnet/sdk:5.0-alpine AS build
WORKDIR /src

COPY *.sln ./

COPY FOSCBot.Bot.Api/*.csproj FOSCBot.Bot.Api/
COPY FOSCBot.Common/*.csproj FOSCBot.Common/
COPY FOSCBot.Core.Domain/*.csproj FOSCBot.Core.Domain/
COPY FOSCBot.Infrastructure.Contract/*.csproj FOSCBot.Infrastructure.Contract/
COPY FOSCBot.Infrastructure.Implementation/*.csproj FOSCBot.Infrastructure.Implementation/
COPY FOSCBot.Persistence.Migrations/*.csproj FOSCBot.Persistence.Migrations/

RUN dotnet restore -r linux-musl-x64

COPY . .
WORKDIR /src/FOSCBot
RUN dotnet publish -c Release -o /app -r linux-musl-x64 --self-contained false --no-restore ../FOSCBot.sln 

FROM mcr.microsoft.com/dotnet/aspnet:5.0-alpine
WORKDIR /app
COPY --from=build /app ./

ENV ASPNETCORE_ENVIRONMENT=Production
ENV ASPNETCORE_URLS http://*:5100
EXPOSE 5100

ENTRYPOINT ["dotnet", "FOSCBot.Bot.Api.dll"]
