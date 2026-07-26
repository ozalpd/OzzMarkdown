@echo off
for /f "tokens=3 delims=<>" %%a in ('findstr /i "<Version>" "..\OzzMarkdown.WPF\OzzMarkdown.WPF.csproj"') do set VERSION=%%a
set TAG=v%VERSION%

cd..
cd
echo Git Status of repository before tagging:
git status

echo .
echo .
echo List of tags in this repository:
git tag

for /f "delims=" %%t in ('git tag -l %TAG%') do set EXISTING_TAG=%%t
if defined EXISTING_TAG (
    echo .
    echo Tag %TAG% already exists in this repository. Aborting.
    pause
    exit /b 1
)

echo .
echo *--------------------------------
echo *
echo * About to execute git tag %TAG% in this repository...
set /p "CONFIRM=* Are you sure you want to continue? (Y to confirm, any other key to cancel): "
echo *
echo *--------------------------------
echo .
if /i not "%CONFIRM%"=="Y" goto :cancelled

echo on
git tag %TAG%
git push origin %TAG%
@echo off
pause
goto :eof

:cancelled
echo Cancelled.
echo .
pause