@echo off
for /f "tokens=3 delims=<>" %%a in ('findstr /i "<Version>" "..\OzzMarkdown.WPF\OzzMarkdown.WPF.csproj"') do set VERSION=%%a
echo Detected version %VERSION%

setlocal enabledelayedexpansion

set NEWLINE=#define MyAppVersion "%VERSION%"

(for /f "usebackq delims=" %%a in ("OzzMarkdown.Setup.iss") do (
    set "line=%%a"
    echo !line:#define MyAppVersion "0.0.0"=%NEWLINE%!
)) > OzzMarkdown.Setup.tmp

set SOURCE=..\OzzMarkdown.WPF\bin\Release\net10.0-windows
set TARGET=..\Installers\OzzMarkdown\

echo Cleaning old files...
echo ...
del "%TARGET%\*" /S /Q /F
echo Preparing installer files...
echo ...
move /Y OzzMarkdown.Setup.tmp "%TARGET%\..\OzzMarkdown.Setup.iss"


echo ...
xcopy "%SOURCE%\*.dll" "%TARGET%" /Y
xcopy "%SOURCE%\*.exe" "%TARGET%" /Y
xcopy "%SOURCE%\*.json" "%TARGET%" /Y
xcopy "%SOURCE%\runtimes\win-x64\native\." "%TARGET%\runtimes\win-x64\native\" /E /Y
xcopy "%SOURCE%\tr\." "%TARGET%\tr\" /E /Y

echo Creating portable ZIP package...
echo ...

powershell.exe -NoProfile -ExecutionPolicy Bypass -Command ^
    "Compress-Archive -Path '%TARGET%\*' -DestinationPath '%TARGET%\..\OzzMarkdown_%VERSION%_Portable.zip' -Force"

echo Portable ZIP created.
echo Done.

pause