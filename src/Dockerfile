FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

COPY *.sln ./

COPY FOSCBot.Bot/*.csproj FOSCBot.Bot/
COPY FOSCBot.Common/*.csproj FOSCBot.Common/
COPY FOSCBot.Core.Domain/*.csproj FOSCBot.Core.Domain/
COPY FOSCBot.Infrastructure.Contract/*.csproj FOSCBot.Infrastructure.Contract/
COPY FOSCBot.Infrastructure.Implementation/*.csproj FOSCBot.Infrastructure.Implementation/
RUN dotnet restore -r linux-musl-x64

COPY . .
WORKDIR /src/FOSCBot
RUN dotnet publish -c Release -o /app -r linux-musl-x64 --self-contained false --no-restore ../FOSCBot.sln 

FROM mcr.microsoft.com/dotnet/aspnet:6.0
RUN apt-get update -y && apt-get install python3.9 pip -y
COPY ./requirements.txt ./
RUN pip install --upgrade pip && \
    pip install -r requirements.txt
 
ARG BOT_VERSION

WORKDIR /app
COPY --from=build /app ./

ENV ASPNETCORE_ENVIRONMENT=Production
ENV ASPNETCORE_URLS http://*:5100
ENV BOT_VERSION $BOT_VERSION
ENV INTEROP_PYTHON="/usr/lib/x86_64-linux-gnu/libpython3.9.so"

EXPOSE 5100

ENTRYPOINT ["dotnet", "FOSCBot.Bot.dll"]
