@echo on
SET CONFIG=%1
IF [%1]==[] SET CONFIG=Release
SET LIB=net45
SET VER=v4.5

MD Bam.Net.Yaml\lib\%LIB%
copy /Y .\BuildOutput\%CONFIG%\%VER%\Bam.Net.Yaml.dll Bam.Net.Yaml\lib\%LIB%\Bam.Net.Yaml.dll
copy /Y .\BuildOutput\%CONFIG%\%VER%\Bam.Net.Yaml.xml Bam.Net.Yaml\lib\%LIB%\Bam.Net.Yaml.xml