monopath-emulator
=================

A launcher for .NET executables that adds support for the `MONO_PATH` environment variable when using the .NET Framework.

The Mono documentation recommends not changing the `MONO_PATH` environment variable. However, it is useful for injecting dependencies at runtime, e.g. when using [Zero Install](https://0install.net/).

Building
--------
Run `build.cmd` to generate these executables:
* `monopath-emulator.clr2.exe` for .NET 2.0-3.5 command-line applications
* `monopath-emulator.clr4.exe` for .NET 4.0+ command-line applications
* `monopath-emulator-win.clr2.exe` for .NET 2.0-3.5 GUI applications
* `monopath-emulator-win.clr4.exe` for .NET 4.0+ GUI applications

Usage
-----
You can manually invoke monopath-emulator like this:
```
set MONO_PATH=C:\Directory\Containing\Library
monopath-emulator[-win].(clr2|clr4).exe Application.exe
```

monopath-emulator is usually used in [Zero Install feeds](https://docs.0install.net/basics/) like this:
```xml
<command name="run" path="Application.exe">
	<runner interface="https://apps.0install.net/dotnet/clr-monopath.xml" />
</command>
<requires interface="http://some/library.xml">
	<environment name="MONO_PATH" insert="." />
</requires>
```
