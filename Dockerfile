FROM mcr.microsoft.com/dotnet/sdk:8.0-preview AS build
WORKDIR /app

COPY *.sln ./
COPY */*.csproj ./

RUN for file in $(ls *.csproj); do mkdir -p ${file%.*}/ && mv $file ${file%.*}/; done
RUN dotnet restore

COPY . .
RUN dotnet build
RUN dotnet publish Todos/Todos.csproj -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:8.0-preview AS runtime
WORKDIR /app
EXPOSE 5000
COPY --from=build /app/out .
ENTRYPOINT [ "dotnet", "Todos.dll" ]
