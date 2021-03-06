@ECHO OFF

set /P key1="Entrez la clef d'encodage 1: "
set /P key2="Entrez la clef d'encodage 2: "
set /P key3="Entrez la clef d'encodage 3: "
set /P key4="Entrez la clef d'encodage 4: "

echo First pass...

for %%f in (unlocked/*.*) do (
  FileEncoder2.exe %key1% %key2% %key3% %key4% "unlocked/%%f" "%temp%/%%f.tmp"
)

echo Second pass...

for %%f in (unlocked/*.*) do (
  FileEncoder2.exe %key1% %key2% %key3% %key4% "%temp%/%%f.tmp" "locked/%%f"
  del /Q /F "%temp%\%%f.tmp"
)

set key1=0
set key2=0
set key3=0
set key4=0

pause