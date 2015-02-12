$solution = Get-ChildItem -Include "*.sln" -Recurse
$msbuild = "C:\WINDOWS\Microsoft.NET\Framework\v4.0.30319\MSBuild.exe"
Invoke-Expression "$msbuild $solution /p:Configuration=Release /t:rebuild /v:minimal /nologo"

# Create package
$files = Get-ChildItem -Exclude "*test*" -Include "*.csproj" -Recurse
foreach ($file in $files)
{
  .\.nuget\nuget.exe pack $file -OutputDirectory .nuget -Prop Configuration=Release -Verbosity detailed
}