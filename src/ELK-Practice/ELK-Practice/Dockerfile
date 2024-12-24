# Use the official ASP.NET Core runtime as a parent image
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Use the SDK image to build the app
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["ELK-Practice/ELK-Practice.csproj", "ELK-Practice/"]
RUN dotnet restore "ELK-Practice/ELK-Practice.csproj"
COPY . .
WORKDIR "/src/ELK-Practice"
RUN dotnet build "ELK-Practice.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ELK-Practice.csproj" -c Release -o /app/publish

# Use the base image to run the app
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ELK-Practice.dll"]
