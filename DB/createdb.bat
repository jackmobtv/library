echo off

rem batch file to run a script to create a database

sqlcmd -S localhost -E -i LibraryDB.sql

echo .
echo if no error messages appear above, the db was created

pause