@echo off

:: Goes to target directory ::
cd /d "C:\Program Files (x86)\Steam\steamapps\common\Snotty's Sewer"

:: Downloads and extracts BepInEx ::
curl -L -o "BepInEx.zip" "https://github.com/BepInEx/BepInEx/releases/latest/download/BepInEx_win_x64_5.4.23.3.zip"
tar -xf "BepInEx.zip" -C .
del /f /q "BepInEx.zip"

:: Runs Snotty's Sewer to init BepInEx ::
.\SnottysSewer.exe

:: Downloads and places the hacks into the plugins folder ::
curl -L -o "BepInEx\Plugins\SnottysHacks.dll" "https://github.com/PashaBibko/SnottysHacks/releases/latest/download/SnottysHacks.dll"
