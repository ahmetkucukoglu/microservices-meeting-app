#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Meeting.Groups.Consumer/Meeting.Groups.Consumer.csproj", "Meeting.Groups.Consumer/"]
COPY ["Meeting.Groups.Core/Meeting.Groups.Core.csproj", "Meeting.Groups.Core/"]
COPY ["Meeting.Groups.Infrastructure/Meeting.Groups.Infrastructure.csproj", "Meeting.Groups.Infrastructure/"]
RUN dotnet restore "Meeting.Groups.Consumer/Meeting.Groups.Consumer.csproj"
COPY . .
WORKDIR "/src/Meeting.Groups.Consumer"
RUN dotnet build "Meeting.Groups.Consumer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Meeting.Groups.Consumer.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Meeting.Groups.Consumer.dll"]