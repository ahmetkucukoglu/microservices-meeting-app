#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Meeting.Users.API/Meeting.Users.API.csproj", "Meeting.Users.API/"]
COPY ["Meeting.Users.Core/Meeting.Users.Core.csproj", "Meeting.Users.Core/"]
COPY ["Meeting.Users.Infrastructure/Meeting.Users.Infrastructure.csproj", "Meeting.Users.Infrastructure/"]
RUN dotnet restore "Meeting.Users.API/Meeting.Users.API.csproj"
COPY . .
WORKDIR "/src/Meeting.Users.API"
RUN dotnet build "Meeting.Users.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Meeting.Users.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Meeting.Users.API.dll"]