$libsPath = 'c:\Games\World of Sea Battle\default\game\Libs'
$dlls = Get-ChildItem -Path $libsPath -Filter *.dll
foreach ($dll in $dlls) {
    try {
        [System.Reflection.Assembly]::LoadFrom($dll.FullName) > $null
    } catch {
        # ignore load errors for dependencies loaded out of order
    }
}

$asm = [System.Reflection.Assembly]::LoadFrom('c:\Games\World of Sea Battle\default\game\Libs\Common.dll')
try {
    $types = $asm.GetTypes()
    Write-Host "--- Auction Types ---"
    $types | Where-Object { $_.Name -like "*Auction*" -or $_.Name -like "*Market*" } | ForEach-Object {
        Write-Host $_.FullName
        # Print properties
        $_.GetProperties() | ForEach-Object {
            Write-Host "  Prop: $($_.PropertyType.Name) $($_.Name)"
        }
        $_.GetFields() | ForEach-Object {
            Write-Host "  Field: $($_.FieldType.Name) $($_.Name)"
        }
    }
} catch [System.Reflection.ReflectionTypeLoadException] {
    Write-Host "ReflectionTypeLoadException encountered. Loader exceptions:"
    $_.Exception.LoaderExceptions | ForEach-Object { Write-Host $_.Message }
}
