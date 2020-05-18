#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Meeting.GatewayAPI/Meeting.GatewayAPI.csproj", "Meeting.GatewayAPI/"]
RUN dotnet restore "Meeting.GatewayAPI/Meeting.GatewayAPI.csproj"
COPY . .
WORKDIR "/src/Meeting.GatewayAPI"
RUN dotnet build "Meeting.GatewayAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Meeting.GatewayAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Meeting.GatewayAPI.dll"]