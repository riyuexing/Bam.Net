@echo on
SET CONFIG=%1
IF [%1]==[] SET CONFIG=Release
SET LIB=net462
SET VER=v4.6.2

MD Bam.Net.Javascript\lib\%LIB%
copy /Y .\BuildOutput\%CONFIG%\%VER%\Bam.Net.Javascript.dll Bam.Net.Javascript\lib\%LIB%\Bam.Net.Javascript.dll
copy /Y .\BuildOutput\%CONFIG%\%VER%\Bam.Net.Javascript.xml Bam.Net.Javascript\lib\%LIB%\Bam.Net.Javascript.xml