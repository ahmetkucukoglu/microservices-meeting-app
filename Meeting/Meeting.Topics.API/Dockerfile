#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Meeting.Topics.API/Meeting.Topics.API.csproj", "Meeting.Topics.API/"]
COPY ["Meeting.Topics.Core/Meeting.Topics.Core.csproj", "Meeting.Topics.Core/"]
COPY ["Meeting.Topics.Infrastructure/Meeting.Topics.Infrastructure.csproj", "Meeting.Topics.Infrastructure/"]
RUN dotnet restore "Meeting.Topics.API/Meeting.Topics.API.csproj"
COPY . .
WORKDIR "/src/Meeting.Topics.API"
RUN dotnet build "Meeting.Topics.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Meeting.Topics.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Meeting.Topics.API.dll"]