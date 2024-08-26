FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY *.sln ./

COPY FOSCBot.Bot/*.csproj FOSCBot.Bot/
COPY FOSCBot.Core/*.csproj FOSCBot.Core/
COPY FOSCBot.Infrastructure.Contract/*.csproj FOSCBot.Infrastructure.Contract/
COPY FOSCBot.Infrastructure.Implementation/*.csproj FOSCBot.Infrastructure.Implementation/
RUN dotnet restore -r linux-musl-x64

COPY . .
WORKDIR /src/FOSCBot
RUN dotnet publish -c Release -o /app -r linux-musl-x64 --self-contained false --no-restore ../FOSCBot.sln 

FROM mcr.microsoft.com/dotnet/aspnet:8.0

ARG BOT_VERSION

WORKDIR /app
COPY --from=build /app ./

ENV ASPNETCORE_ENVIRONMENT=Production
ENV ASPNETCORE_URLS http://*:5100
ENV BOT_VERSION $BOT_VERSION

EXPOSE 5100

ENTRYPOINT ["dotnet", "FOSCBot.Bot.dll"]
