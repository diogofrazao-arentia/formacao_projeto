param(
    [switch]$SkipRestore,
    [int]$Port = 5127
)

$ErrorActionPreference = "Stop"

Set-Location (Join-Path $PSScriptRoot "..")

if (-not (Get-Command docker -ErrorAction SilentlyContinue)) {
    throw "Docker CLI was not found. Install Docker Desktop and try again."
}

$connectionString = "Server=localhost,1433;Database=InternalTicketManager;User Id=sa;Password=Your_password123;Encrypt=False;TrustServerCertificate=True"
$url = "http://localhost:$Port"

Write-Host "Starting SQL Server container..."
docker compose up -d sqlserver | Out-Host

Write-Host "Using SQL Server connection for this session..."
$env:ConnectionStrings__DefaultConnection = $connectionString
$env:ASPNETCORE_URLS = $url
$env:ASPNETCORE_ENVIRONMENT = "Development"

if (-not $SkipRestore) {
    Write-Host "Restoring solution packages..."
    dotnet restore InternalTicketManager.sln
}

Write-Host "Starting web app on $url ..."
Write-Host "Tip: press Ctrl+C to stop the app."
dotnet run --project src/TicketManager.Web --no-launch-profile --urls $url
