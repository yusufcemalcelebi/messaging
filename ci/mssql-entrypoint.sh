#!/bin/bash

sh -c " 
echo 'Sleeping 15 seconds before running setup script...'
sleep 15s

echo 'Starting setup script...'

#run the setup script to create the DB and the schema in the DB
/opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P Mypassword!1 -d master -i /usr/src/myscript/schema.sql

echo 'Finished setup script.'
exit
" & 
exec /opt/mssql/bin/sqlservr
