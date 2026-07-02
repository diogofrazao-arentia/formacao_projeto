param(
    [switch]$SkipRestore,
    [int]$Port = 5127
)

$ErrorActionPreference = "Stop"

Set-Location (Join-Path $PSScriptRoot "..")

$url = "http://localhost:$Port"
$databasePaths = @(
    (Join-Path (Get-Location) "InternalTicketManager.db"),
    (Join-Path (Get-Location) "src/TicketManager.Web/InternalTicketManager.db")
)

Write-Host "Using SQLite database for this session..."
foreach ($databasePath in $databasePaths) {
    Remove-Item $databasePath, "$databasePath-wal", "$databasePath-shm" -ErrorAction SilentlyContinue
}
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
