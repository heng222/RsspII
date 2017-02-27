
REM VS Settings
set VSDir="C:\Program Files\Microsoft Visual Studio 10.0\Common7\IDE\devenv.exe"
set VSVersion=vs100

REM Set Solution Configration[Debug/Release] AND Solution platform[win32/x64/Any CPU]
set Config=Release
set Platform=Win32

REM Project Name
set SolutionName=BJMT.RsspII
set ProjectName=BJMT.RsspII4cplus

REM Solution Directory
set SolutionDir=..\

REM Project Directory
set ProjectDir=..\%ProjectName%\

REM *.rc Path
set RcPath=%ProjectDir%%ProjectName%.rc

REM Update
svn update %SolutionDir%

REM Backup AssemblyInfo.cs
copy %RcPath% %RcPath%.bak /y

REM General TPL file
RC2SVNTPL %RcPath% %RcPath%.tpl 10

REM Create a new Assemblyinfo using SVN Revision
SubWcRev %ProjectDir% %RcPath%.tpl %RcPath% 

REM Build solution with VisualStdio
set LogFile=".\%VSVersion%_%Config%_%Platform%.txt"
IF EXIST %LogFile% DEL %LogFile%
%VSDir% %SolutionDir%%SolutionName%_%VSVersion%.sln /ReBuild "%Config%|%Platform%" /Project %ProjectName% /OUT %LogFile%


REM Recovery
DEL %RcPath%
REN %RcPath%.bak %ProjectName%.rc

Pause