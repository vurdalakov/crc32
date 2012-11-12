@echo off

set ConfigurationName=%1
set TargetDir=%2

set ilmerge="C:\Program Files\Microsoft\ILMerge\ILMerge.exe"

if %ConfigurationName%==Release (
    %ilmerge% /out:%TargetDir%crc32.exe %TargetDir%crc32exe.exe %TargetDir%vurdalakovdotnet.dll
)
