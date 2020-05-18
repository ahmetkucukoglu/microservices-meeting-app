#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Meeting.Events.Consumer/Meeting.Events.Consumer.csproj", "Meeting.Events.Consumer/"]
COPY ["Meeting.Events.Core/Meeting.Events.Core.csproj", "Meeting.Events.Core/"]
COPY ["Meeting.Events.Infrastructure/Meeting.Events.Infrastructure.csproj", "Meeting.Events.Infrastructure/"]
RUN dotnet restore "Meeting.Events.Consumer/Meeting.Events.Consumer.csproj"
COPY . .
WORKDIR "/src/Meeting.Events.Consumer"
RUN dotnet build "Meeting.Events.Consumer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Meeting.Events.Consumer.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Meeting.Events.Consumer.dll"]