@ECHO OFF
REM Used to create a release package for Simpit
REM Arguments : ProjectDir OutDir

set ProjectDir=%1
set OutDir=%2

set "MyKSPDIR=%KSPDIR%"

if not defined MyKSPDIR (
  echo KSPDIR is not set, I cannot make a release
  exit /b 1
)

REM Read the version number
for /f "delims== tokens=1,2" %%G in (%ProjectDir%VERSION.txt) do set %%G=%%H

echo Version read %MAJOR%.%MINOR%.%PATCH%.%BUILD%

set OUTPUT_FOLDER="%ProjectDir%\..\..\releases\KerbalSimpitRefactored-v%MAJOR%.%MINOR%.%PATCH%\KerbalSimpitRefactored"

REM clean the output folder by removing and recreating it
if exist %OUTPUT_FOLDER% RMDIR /S /Q %OUTPUT_FOLDER%
MKDIR %OUTPUT_FOLDER%

REM Create the version file
(
echo {
echo   "NAME": "Kerbal Simpit Refactored",
echo   "DOWNLOAD": "https://github.com/rettoph/KerbalSimpitRefactored/releases",
echo   "GITHUB": {
echo     "USERNAME": "Simpit-team",
echo     "REPOSITORY": "KerbalSimpitRevamped"
echo   },
echo   "VERSION": {
echo     "MAJOR": %MAJOR%,
echo     "MINOR": %MINOR%,
echo     "PATCH": %PATCH%,
echo     "BUILD": %BUILD%
echo   },
echo   "KSP_VERSION_MIN": {
echo     "MAJOR": %KSPMAJOR%,
echo     "MINOR": %KSPMINOR%,
echo     "PATCH": %KSPPATCH%
echo   },
echo   "KSP_VERSION_MAX": {
echo     "MAJOR": %KSPMAJOR%,
echo     "MINOR": %KSPMINOR%,
echo     "PATCH": 99
echo   }
echo }
)>"%OUTPUT_FOLDER%\KerbalSimpitRefactored.version"

REM Copy all the files
xcopy /q %ProjectDir%%OutDir% %OUTPUT_FOLDER% 

REM now include the Arduino lib.

set ARDUINOLIB_FOLDER=%ProjectDir%\..\..\libraries\KerbalSimpitRefactored-Arduino\

echo %ARDUINOLIB_FOLDER%

if not exist %ARDUINOLIB_FOLDER% (
  echo Cannot locate the Arduino libs. Cannot create a release.
  exit /b 2
)

xcopy /q /S %ARDUINOLIB_FOLDER%src %OUTPUT_FOLDER%\KerbalSimpitRefactored-Arduino\src\
xcopy /q /S %ARDUINOLIB_FOLDER%libraries %OUTPUT_FOLDER%\KerbalSimpitRefactored-Arduino\libraries\
xcopy /q /S %ARDUINOLIB_FOLDER%examples %OUTPUT_FOLDER%\KerbalSimpitRefactored-Arduino\examples\
xcopy /q %ARDUINOLIB_FOLDER%keywords.txt %OUTPUT_FOLDER%\KerbalSimpitRefactored-Arduino
xcopy /q %ARDUINOLIB_FOLDER%library.properties %OUTPUT_FOLDER%\KerbalSimpitRefactored-Arduino
xcopy /q %ARDUINOLIB_FOLDER%CHANGELOG.rst %OUTPUT_FOLDER%\KerbalSimpitRefactored-Arduino
xcopy /q %ARDUINOLIB_FOLDER%DEVNOTES.md %OUTPUT_FOLDER%\KerbalSimpitRefactored-Arduino
xcopy /q %ARDUINOLIB_FOLDER%LICENSE.md %OUTPUT_FOLDER%\KerbalSimpitRefactored-Arduino
xcopy /q %ARDUINOLIB_FOLDER%README.rst %OUTPUT_FOLDER%\KerbalSimpitRefactored-Arduino
xcopy /q %ARDUINOLIB_FOLDER%ressource_list.txt %OUTPUT_FOLDER%\KerbalSimpitRefactored-Arduino


REM now compress it
echo %ProjectDir%\..\..\releases\KerbalSimpitRefactored-v%MAJOR%.%MINOR%.%PATCH%
"C:\Program Files\7-Zip\7z.exe" a -tzip %ProjectDir%\..\..\releases\KerbalSimpitRefactored-v%MAJOR%.%MINOR%.%PATCH%.zip %ProjectDir%\..\..\releases\KerbalSimpitRefactored-v%MAJOR%.%MINOR%.%PATCH%\*
