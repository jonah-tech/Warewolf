powershell -NoProfile -NoLogo -ExecutionPolicy Bypass -File "%~dp0..\..\..\..\Compile.ps1" -AcceptanceTesting
cd /d "%~dp0..\..\..\..\bin\AcceptanceTesting"
powershell -NoProfile -ExecutionPolicy Bypass -NoExit -File "%~dp0..\TestRun.ps1" -Projects Warewolf.UI.Load.Specs -Category StudioOpenningLongWorkflowUILoadTest -PreTestRunScript 'StartAs.ps1 -Cleanup -Anonymous -ResourcesPath LoadTests'