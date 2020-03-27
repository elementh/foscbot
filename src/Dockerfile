FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src

COPY *.sln ./

COPY FOSCBot.Bot.Api/*.csproj FOSCBot.Bot.Api/
COPY FOSCBot.Common/*.csproj FOSCBot.Common/
COPY FOSCBot.Core.Domain/*.csproj FOSCBot.Core.Domain/

RUN dotnet restore -r linux-musl-x64

COPY . .
WORKDIR /src/FOSCBot
RUN dotnet publish -c Release -o /app -r linux-musl-x64 --self-contained false --no-restore ../FOSCBot.sln 

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-alpine
WORKDIR /app
COPY --from=build /app ./

ENV ASPNETCORE_ENVIRONMENT=Production
ENV ASPNETCORE_URLS http://*:5100
EXPOSE 5100

ENTRYPOINT ["dotnet", "FOSCBot.Bot.Api.dll"]
