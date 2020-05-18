#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Meeting.Events.API/Meeting.Events.API.csproj", "Meeting.Events.API/"]
COPY ["Meeting.Events.Core/Meeting.Events.Core.csproj", "Meeting.Events.Core/"]
COPY ["Meeting.Events.Infrastructure/Meeting.Events.Infrastructure.csproj", "Meeting.Events.Infrastructure/"]
RUN dotnet restore "Meeting.Events.API/Meeting.Events.API.csproj"
COPY . .
WORKDIR "/src/Meeting.Events.API"
RUN dotnet build "Meeting.Events.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Meeting.Events.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Meeting.Events.API.dll"]