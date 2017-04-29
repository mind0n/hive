

@echo off

echo Create Service %1ServiceLoader.exe

sc delete a
sc create a binPath= %1ServiceLoader.exe

pause