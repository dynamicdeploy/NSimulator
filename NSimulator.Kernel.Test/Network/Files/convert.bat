@echo off
setlocal ENABLEDELAYEDEXPANSION
set /a COUNT=0
for /f "delims=" %%i in ('dir /b /s *.xml') do (
  set FROM=%%~ni.xml
  set TO=%%~ni.t
  if exist !TO! (
    echo 1>&2 File "!TO!" already exists.
    echo 1>&2 "!FROM!" will be NOT processed.
  ) else (
    netconv.exe "/input:%%~npi.xml" "/output:!TO!"
    echo !FROM!
    set /a COUNT=!COUNT!+1
  )
)
echo !COUNT! files processed.
endlocal
