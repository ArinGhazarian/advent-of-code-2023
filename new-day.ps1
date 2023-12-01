#!/usr/bin/env pwsh

param([String][Parameter(Mandatory = $true)]$NewDayDirName)

if (Test-Path -Path $NewDayDirName) {
    Write-Error "Directory ${NewDayDirName} already exists"
    exit
}

New-Item -ItemType "directory" -Path ${NewDayDirName}
New-Item -ItemType "file" -Path ${NewDayDirName} -Name "input.txt"
dotnet new console -o ${NewDayDirName}
dotnet sln add ${NewDayDirName}