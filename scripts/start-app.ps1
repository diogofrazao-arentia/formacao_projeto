param(
    [switch]$SkipRestore,
    [int]$Port = 5127
)

$ErrorActionPreference = "Stop"

Set-Location (Join-Path $PSScriptRoot "..")

$url = "http://localhost:$Port"

Write-Host "Using SQLite database for this session..."
$env:ConnectionStrings__DefaultConnection = "Data Source=InternalTicketManager.db"
$env:ASPNETCORE_URLS = $url
$env:ASPNETCORE_ENVIRONMENT = "Development"

if (-not $SkipRestore) {
    Write-Host "Restoring solution packages..."
    dotnet restore InternalTicketManager.sln
}

Write-Host "Starting web app on $url ..."
Write-Host "Tip: press Ctrl+C to stop the app."
dotnet run --project src/TicketManager.Web --no-launch-profile --urls $url
