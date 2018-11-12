Param(
  [String]$Configuration = "Release"
)

Write-Host "Started processing 'Todo' WebAPI..." -foregroundcolor "Magenta"

Write-Host "Restoring 'Todo' components...."
dotnet restore "WebApi.Todo"
dotnet restore "WebApi.Todo.Test"

Write-Host "Building 'Todo' WebAPI..." -foregroundcolor "Magenta"
dotnet build --configuration $Configuration --framework netcoreapp2.1 "WebApi.Todo.sln"

Write-Host "Cleaning 'WebApi.Todo' dist folder..." -foregroundcolor "Magenta"
$CurrentDir = [System.IO.Path]::GetDirectoryName($myInvocation.MyCommand.Path)
$DistFolderPath = "$CurrentDir\dist"
if (Test-Path $DistFolderPath) {
  Remove-Item $DistFolderPath -Force -Recurse
}

Write-Host "Publishing 'Todo' WebAPI..." -foregroundcolor "Magenta"
dotnet publish "WebAPI.Todo" --output $DistFolderPath --configuration Release --framework netcoreapp2.1

Write-Host "`n`nBuild of 'Todo' WebAPI is COMPLETE!`n`n" -foregroundcolor "Green"