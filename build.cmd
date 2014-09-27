@echo off
"%windir%\Microsoft.NET\Framework\v3.5\csc.exe" /nologo /win32manifest:"%~dp0app.manifest" /target:exe /out:"%~dp0monopath-emulator.clr2.exe" "%~dp0monopath-emulator.cs"
"%windir%\Microsoft.NET\Framework\v4.0.30319\csc.exe" /nologo /win32manifest:"%~dp0app.manifest" /target:exe /out:"%~dp0monopath-emulator.clr4.exe" "%~dp0monopath-emulator.cs" /nowarn:0618
"%windir%\Microsoft.NET\Framework\v3.5\csc.exe" /nologo /win32manifest:"%~dp0app.manifest" /target:winexe /out:"%~dp0monopath-emulator-win.clr2.exe" "%~dp0monopath-emulator.cs"
"%windir%\Microsoft.NET\Framework\v4.0.30319\csc.exe" /nologo /win32manifest:"%~dp0app.manifest" /target:winexe /out:"%~dp0monopath-emulator-win.clr4.exe" "%~dp0monopath-emulator.cs" /nowarn:0618