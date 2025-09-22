@echo off
cd /d "%~dp0"
if not exist bin mkdir bin
csc /nologo /target:winexe /out:bin\bricks.exe src\*.cs