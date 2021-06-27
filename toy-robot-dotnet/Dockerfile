FROM microsoft/dotnet:3.1-sdk AS build
WORKDIR /app

COPY toy-robot-dotnet.csproj .
RUN dotnet restore  toy-robot-dotnet.csproj

COPY . .
RUN dotnet publish -c Release -o out


FROM microsoft/dotnet:3.1-aspnetcore-runtime AS runtime
WORKDIR /App
COPY bin/Release/netcoreapp3.1/publish/ App/

ENTRYPOINT ["dotnet", "toy-robot-dotnet.dll"]
