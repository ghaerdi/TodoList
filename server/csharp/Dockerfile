FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /home/app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /home/source
COPY ["csharp.csproj", "."]
RUN dotnet restore "./csharp.csproj"
COPY . .
WORKDIR "/home/source/."
RUN dotnet build "csharp.csproj" -c Release -o /home/app/build

FROM build AS publish
RUN dotnet publish "csharp.csproj" -c Release -o /home/app/publish

FROM base AS final
WORKDIR /home/app
COPY --from=publish /home/app/publish .
ENTRYPOINT ["dotnet", "csharp.dll"]
