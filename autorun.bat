:start
@echo off

:make_exe
robocopy .\Test\bin\debug ..\SayAgainGame\content\bin\debug /E
robocopy .\Test\Art ..\SayAgainGame\content\Art /E
robocopy .\Test\Fonts ..\SayAgainGame\content\Fonts /E
robocopy .\Test\Sounds ..\SayAgainGame\content\Sounds /E
robocopy .\Test\ ..\SayAgainGame\content *.json /E
<<<<<<< HEAD
Xcopy .\installer.bat ..\SayAgainGame

=======

@echo off
set SCRIPT="%TEMP%\%RANDOM%-%RANDOM%-%RANDOM%-%RANDOM%.vbs"
echo Set oWS = WScript.CreateObject("WScript.Shell") >> %SCRIPT%
echo sLinkFile = "%USERPROFILE%\Desktop\SA.lnk" >> %SCRIPT%
echo Set oLink = oWS.CreateShortcut(sLinkFile) >> %SCRIPT%
echo oLink.TargetPath = "%userprofile%\Desktop\SayAgainGame\content\bin\debug\Test.exe" >> %SCRIPT%
echo oLink.IconLocation = "%userprofile%\Desktop\SayAgainGame\content\Art\exe_icon.ico" >> %SCRIPT%
echo oLink.WorkingDirectory = "%userprofile%\Desktop\SayAgainGame\content\bin\debug" >> %SCRIPT%
echo oLink.Save >> %SCRIPT%
cscript /nologo %SCRIPT%
del %SCRIPT%
REM start /b "" cmd /c del "%~f0"&exit /b"

>>>>>>> d2f3f61427f1fefd8a8acd2b8be5e93bb78df735
exit

