@echo off

SET LIB=net40
SET VER=v4.0
SET NEXT=NEXT
GOTO COPY

:NEXT
SET LIB=net45
SET VER=v4.5
SET NEXT=END
GOTO COPY

:COPY
MD Bam.Net.Data.Repositories\lib\%LIB%
copy /Y .\BuildOutput\Release\%VER%\Bam.Net.Data.Repositories.dll Bam.Net.Data.Repositories\lib\%LIB%\Bam.Net.Data.Repositories.dll
copy /Y .\BuildOutput\Release\%VER%\Bam.Net.Data.Repositories.xml Bam.Net.Data.Repositories\lib\%LIB%\Bam.Net.Data.Repositories.xml
GOTO %NEXT%

:END


