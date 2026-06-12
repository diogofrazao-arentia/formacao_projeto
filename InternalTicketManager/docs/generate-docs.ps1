param(
    [ValidateSet("Debug", "Release")]
    [string]$Configuration = "Debug",

    [switch]$Open
)

Set-StrictMode -Version Latest
$ErrorActionPreference = "Stop"

$root = Split-Path -Parent $PSScriptRoot
$projectPath = Join-Path $root "src/TicketManager.Web/TicketManager.Web.csproj"
$xmlPath = Join-Path $root "src/TicketManager.Web/bin/$Configuration/net8.0/TicketManager.Web.xml"
$outputDir = Join-Path $PSScriptRoot "generated"
$outputPath = Join-Path $outputDir "index.html"

dotnet build $projectPath --configuration $Configuration | Out-Host

if (-not (Test-Path $xmlPath)) {
    throw "Documentation XML file was not generated: $xmlPath"
}

New-Item -ItemType Directory -Force -Path $outputDir | Out-Null

[xml]$xml = Get-Content -Raw $xmlPath
$members = @($xml.doc.members.member) | Sort-Object name

function Convert-DocText {
    param([object]$Node)

    if ($null -eq $Node) {
        return ""
    }

    if ($Node -is [string]) {
        return (($Node -replace "\s+", " ").Trim())
    }

    return (($Node.InnerText -replace "\s+", " ").Trim())
}

function Get-Kind {
    param([string]$Name)

    switch ($Name.Substring(0, 2)) {
        "T:" { return "Type" }
        "P:" { return "Property" }
        "M:" { return "Method" }
        "F:" { return "Field" }
        default { return "Member" }
    }
}

function Get-DisplayName {
    param([string]$Name)

    return $Name.Substring(2)
}

$rows = foreach ($member in $members) {
    $summary = Convert-DocText $member.SelectSingleNode("summary")
    $remarks = Convert-DocText $member.SelectSingleNode("remarks")
    $params = @(@($member.SelectNodes("param")) | ForEach-Object {
        if ($null -ne $_.name) {
            "<li><code>$([System.Net.WebUtility]::HtmlEncode($_.name))</code>: $([System.Net.WebUtility]::HtmlEncode((Convert-DocText $_)))</li>"
        }
    })
    $returns = Convert-DocText $member.SelectSingleNode("returns")

    $details = ""
    if ($remarks) {
        $details += "<p class='remarks'>$([System.Net.WebUtility]::HtmlEncode($remarks))</p>"
    }
    if ($params.Count -gt 0) {
        $details += "<h3>Parameters</h3><ul>$($params -join '')</ul>"
    }
    if ($returns) {
        $details += "<h3>Returns</h3><p>$([System.Net.WebUtility]::HtmlEncode($returns))</p>"
    }

    @"
<article class="member">
  <div class="member-kind">$(Get-Kind $member.name)</div>
  <h2>$([System.Net.WebUtility]::HtmlEncode((Get-DisplayName $member.name)))</h2>
  <p>$([System.Net.WebUtility]::HtmlEncode($summary))</p>
  $details
</article>
"@
}

$generatedAt = Get-Date -Format "yyyy-MM-dd HH:mm:ss"
$html = @"
<!doctype html>
<html lang="en">
<head>
  <meta charset="utf-8">
  <meta name="viewport" content="width=device-width, initial-scale=1">
  <title>Internal Ticket Manager - Code Documentation</title>
  <style>
    :root {
      color-scheme: light;
      font-family: "Segoe UI", Arial, sans-serif;
      line-height: 1.5;
      color: #1f2933;
      background: #f5f7fa;
    }
    body {
      margin: 0;
    }
    header {
      background: #12343b;
      color: #fff;
      padding: 32px max(24px, calc((100vw - 1080px) / 2));
    }
    main {
      max-width: 1080px;
      margin: 0 auto;
      padding: 24px;
    }
    h1, h2, h3 {
      margin: 0;
      letter-spacing: 0;
    }
    h1 {
      font-size: 32px;
    }
    h2 {
      font-size: 20px;
      margin-top: 6px;
      overflow-wrap: anywhere;
    }
    h3 {
      font-size: 14px;
      margin-top: 16px;
    }
    code {
      background: #edf2f7;
      border-radius: 4px;
      padding: 2px 5px;
    }
    .meta {
      margin-top: 8px;
      color: #d7e3e7;
    }
    .member {
      background: #fff;
      border: 1px solid #d9e2ec;
      border-radius: 8px;
      padding: 20px;
      margin-bottom: 16px;
    }
    .member-kind {
      display: inline-block;
      font-size: 12px;
      font-weight: 700;
      text-transform: uppercase;
      color: #0f5132;
      background: #d1e7dd;
      border-radius: 4px;
      padding: 3px 7px;
    }
    .remarks {
      color: #4a5568;
    }
  </style>
</head>
<body>
  <header>
    <h1>Internal Ticket Manager - Code Documentation</h1>
    <p class="meta">Generated from XML documentation comments at $generatedAt.</p>
  </header>
  <main>
    $($rows -join "`n")
  </main>
</body>
</html>
"@

Set-Content -Path $outputPath -Value $html -Encoding UTF8
Write-Host "Documentation generated at: $outputPath"

if ($Open) {
    Invoke-Item $outputPath
}
