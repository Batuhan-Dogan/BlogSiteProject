FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app
COPY ./ProgrammersBlog.Data/*.csproj ./ProgrammersBlog.Data/
COPY ./ProgrammersBlog.Entities/*.csproj ./ProgrammersBlog.Entities/
COPY ./ProgrammersBlog.Shared/*.csproj ./ProgrammersBlog.Shared/
COPY ./ProgrammersBlog.Services/*.csproj ./ProgrammersBlog.Services/
COPY ./ProgrammersBlog.Mvc/*.csproj ./ProgrammersBlog.Mvc/
COPY *.sln .
RUN dotnet restore
COPY . .
RUN dotnet publish ./ProgrammersBlog.Mvc/*.csproj -o /publish/
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build /publish .
ENV ASPNETCORE_URLS="http://*:4505"
ENTRYPOINT [ "dotnet","ProgrammersBlog.Mvc.dll" ]