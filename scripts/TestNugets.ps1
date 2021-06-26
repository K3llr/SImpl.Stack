$nuspecs =  Get-ChildItem -Path ..\*\*.csproj -Recurse -Force;
$nuspecs | Foreach-Object { 
 dotnet pack $_.FullName --version-suffix "local-beta.23" --output "./"
 }
