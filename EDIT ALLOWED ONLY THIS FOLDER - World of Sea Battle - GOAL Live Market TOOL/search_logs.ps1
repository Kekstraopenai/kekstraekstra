$paths = @($env:LOCALAPPDATA, $env:APPDATA, "$env:USERPROFILE\Documents")
foreach ($path in $paths) {
    if (Test-Path $path) {
        Get-ChildItem -Path $path -Recurse -ErrorAction SilentlyContinue | 
            Where-Object { $_.FullName -like "*Sea*Battle*" -or $_.FullName -like "*Wosb*" } | 
            Select-Object FullName, LastWriteTime | 
            ForEach-Object { Write-Host "$($_.FullName) | $($_.LastWriteTime)" }
    }
}
