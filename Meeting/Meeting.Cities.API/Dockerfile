#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Meeting.Cities.API/Meeting.Cities.API.csproj", "Meeting.Cities.API/"]
COPY ["Meeting.Cities.Core/Meeting.Cities.Core.csproj", "Meeting.Cities.Core/"]
COPY ["Meeting.Cities.Infrastructure/Meeting.Cities.Infrastructure.csproj", "Meeting.Cities.Infrastructure/"]
RUN dotnet restore "Meeting.Cities.API/Meeting.Cities.API.csproj"
COPY . .
WORKDIR "/src/Meeting.Cities.API"
RUN dotnet build "Meeting.Cities.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Meeting.Cities.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Meeting.Cities.API.dll"]