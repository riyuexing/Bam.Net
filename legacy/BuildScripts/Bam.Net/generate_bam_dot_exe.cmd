
@echo on
SET CONFIG=%1
IF [%1]==[] SET CONFIG=Release
SET LIB=net462
SET VER=v4.6.2
cd .\BuildOutput\%CONFIG%\v4.6.2
md ..\..\..\BamDotExe\lib\%LIB%\
..\..\..\ilmerge.exe bam.exe BamCore.dll Bam.Net.Analytics.dll Bam.Net.Automation.dll Bam.Net.dll Bam.Net.CommandLine.dll Bam.Net.CoreServices.dll Bam.Net.Data.dll Bam.Net.Data.Model.dll Bam.Net.Data.MsSql.dll Bam.Net.Data.Schema.dll Bam.Net.Data.SQLite.dll Bam.Net.Data.Npgsql.dll Bam.Net.Data.Oracle.dll Bam.Net.Data.MySql.dll Bam.Net.Data.Protobuf.dll Bam.Net.Data.Dynamic.dll Bam.Net.Data.Repositories.dll Bam.Net.Documentation.dll Bam.Net.Drawing.dll Bam.Net.Presentation.Dust.dll Bam.Net.Encryption.dll Bam.Net.Presentation.dll Bam.Net.Incubation.dll Bam.Net.Javascript.dll Bam.Net.Logging.dll Bam.Net.Messaging.dll Bam.Net.Net.dll Bam.Net.Profiguration.dll Bam.Net.Schema.Org.dll Bam.Net.Server.dll Bam.Net.ServiceProxy.dll Bam.Net.Services.dll Bam.Net.Services.Clients.dll Bam.Net.Syndication.dll Bam.Net.Testing.dll Bam.Net.UserAccounts.dll Bam.Net.Yaml.dll /closed /targetplatform:v4 /lib:"C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.6.2" /out:..\..\..\BamDotExe\lib\%LIB%\bam.exe
cd ..\..\..\