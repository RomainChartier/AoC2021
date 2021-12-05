$daynumber = "04"

New-Item -Path "D:\dev\c#\AoC2021\AoC2021.Cli\Data\day$($daynumber).txt" -ItemType File
New-Item -Path "D:\dev\c#\AoC2021\AoC2021.Cli\Data\Test\testday$($daynumber).txt" -ItemType File

(Get-Content -Path "D:\dev\c#\AoC2021\AoC2021.Cli\Template.cs").Replace("Day01", "Day$($daynumber)").Replace("day01", "day$($daynumber)") | Out-File -FilePath "D:\dev\c#\AoC2021\AoC2021.Cli\Day$($daynumber).cs"
(Get-Content -Path "D:\dev\c#\AoC2021\AoC2021.Tests\Template.cs").Replace("Day01", "Day$($daynumber)").Replace("day01", "day$($daynumber)") | Out-File -FilePath "D:\dev\c#\AoC2021\AoC2021.Tests\Day$($daynumber).cs"