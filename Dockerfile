# ---- Build stage ----
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy solution + project files first to leverage Docker layer caching on restore
COPY ["RecruitProApp.sln", "./"]
COPY ["src/RecruitProApp.Domain/RecruitProApp.Domain.csproj", "src/RecruitProApp.Domain/"]
COPY ["src/RecruitProApp.Application/RecruitProApp.Application.csproj", "src/RecruitProApp.Application/"]
COPY ["src/RecruitProApp.Infrastructure/RecruitProApp.Infrastructure.csproj", "src/RecruitProApp.Infrastructure/"]
COPY ["src/RecruitProApp.WebAPI/RecruitProApp.WebAPI.csproj", "src/RecruitProApp.WebAPI/"]
RUN dotnet restore "src/RecruitProApp.WebAPI/RecruitProApp.WebAPI.csproj"

# Copy the rest of the source and publish
COPY . .
RUN dotnet publish "src/RecruitProApp.WebAPI/RecruitProApp.WebAPI.csproj" \
    -c Release -o /app/publish /p:UseAppHost=false

# ---- Runtime stage ----
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "RecruitProApp.WebAPI.dll"]
