#!/usr/bin/env pwsh

# Store.Fares Deployment Script for ASP Monster
# This script helps automate the deployment process

param(
    [string]$Action = "help"
)

$ProjectRoot = Get-Location
$PublishFolder = Join-Path $ProjectRoot "publish"

function Show-Help {
    Write-Host "Store.Fares Deployment Script" -ForegroundColor Cyan
    Write-Host "==============================" -ForegroundColor Cyan
    Write-Host ""
    Write-Host "Usage: .\deploy.ps1 [action]" -ForegroundColor Yellow
    Write-Host ""
    Write-Host "Available Actions:" -ForegroundColor Green
    Write-Host "  help              Show this help message"
    Write-Host "  clean             Remove old publish folder"
    Write-Host "  build             Build project in Release mode"
    Write-Host "  publish           Publish application for production"
    Write-Host "  publish-clean     Clean and publish (recommended)"
    Write-Host "  check             Check deployment readiness"
    Write-Host ""
    Write-Host "Example:"
    Write-Host "  .\deploy.ps1 publish-clean"
    Write-Host ""
}

function Clean-Publish {
    Write-Host "Cleaning old publish folder..." -ForegroundColor Yellow
    if (Test-Path $PublishFolder) {
        Remove-Item $PublishFolder -Recurse -Force
        Write-Host "✓ Cleaned successfully" -ForegroundColor Green
    } else {
        Write-Host "✓ No old publish folder found" -ForegroundColor Green
    }
}

function Build-Release {
    Write-Host "Building project in Release mode..." -ForegroundColor Yellow
    
    Write-Host "Running: dotnet clean -c Release" -ForegroundColor Cyan
    & dotnet clean -c Release
    
    Write-Host "Running: dotnet build -c Release" -ForegroundColor Cyan
    & dotnet build -c Release
    
    if ($LASTEXITCODE -eq 0) {
        Write-Host "✓ Build completed successfully" -ForegroundColor Green
    } else {
        Write-Host "✗ Build failed" -ForegroundColor Red
        exit 1
    }
}

function Publish-App {
    Write-Host "Publishing application for production..." -ForegroundColor Yellow
    
    Write-Host "Running: dotnet publish -c Release -o ./publish --no-build" -ForegroundColor Cyan
    & dotnet publish -c Release -o "./publish" --no-build
    
    if ($LASTEXITCODE -eq 0) {
        Write-Host "✓ Publish completed successfully" -ForegroundColor Green
        Write-Host ""
        Write-Host "Published files location: $PublishFolder" -ForegroundColor Cyan
        Write-Host "Total size: " -ForegroundColor Yellow -NoNewline
        $size = (Get-ChildItem $PublishFolder -Recurse | Measure-Object -Property Length -Sum).Sum / 1MB
        Write-Host "$([Math]::Round($size, 2)) MB" -ForegroundColor Green
    } else {
        Write-Host "✗ Publish failed" -ForegroundColor Red
        exit 1
    }
}

function Publish-Clean {
    Write-Host "Starting clean publish process..." -ForegroundColor Cyan
    Write-Host ""
    
    Clean-Publish
    Write-Host ""
    
    Build-Release
    Write-Host ""
    
    Publish-App
    Write-Host ""
    
    Write-Host "Deployment package ready!" -ForegroundColor Green
    Write-Host ""
    Write-Host "Next Steps:" -ForegroundColor Yellow
    Write-Host "1. Update appsettings.Production.json with your ASP Monster credentials"
    Write-Host "2. Upload all files from '$PublishFolder' folder via FTP"
    Write-Host "3. Run database migrations"
    Write-Host "4. Verify https://your-domain.com is working"
    Write-Host ""
}

function Check-Readiness {
    Write-Host "Checking deployment readiness..." -ForegroundColor Cyan
    Write-Host ""
    
    $Checks = @(
        @{ Name = ".NET SDK installed"; Check = { dotnet --version } },
        @{ Name = "Project file exists"; Check = { Test-Path "Store.Fares.sln" } },
        @{ Name = "appsettings.json exists"; Check = { Test-Path "Store.Fares.Api/appsettings.json" } },
        @{ Name = "appsettings.Production.json exists"; Check = { Test-Path "Store.Fares.Api/appsettings.Production.json" } }
    )
    
    foreach ($check in $Checks) {
        try {
            $result = & $check.Check
            Write-Host "✓ $($check.Name)" -ForegroundColor Green
        } catch {
            Write-Host "✗ $($check.Name)" -ForegroundColor Red
        }
    }
    
    Write-Host ""
}

# Main execution
switch ($Action.ToLower()) {
    "help" { Show-Help }
    "clean" { Clean-Publish }
    "build" { Build-Release }
    "publish" { Publish-App }
    "publish-clean" { Publish-Clean }
    "check" { Check-Readiness }
    default {
        Write-Host "Unknown action: $Action" -ForegroundColor Red
        Write-Host ""
        Show-Help
        exit 1
    }
}
