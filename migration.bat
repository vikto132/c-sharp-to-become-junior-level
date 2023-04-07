@ECHO OFF
:parse
IF "%~1"=="" GOTO endparse
dotnet ef migrations add "%~1" -s project/Migrations  -c JuniorDbContext --verbose --output-dir Junior
SHIFT
GOTO parse
:endparse
