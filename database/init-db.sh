#!/bin/bash

# Run the SQL script to initialize the database
sleep 30 # Wait for SQL Server to be fully ready
/opt/mssql-tools18/bin/sqlcmd -S db -U sa -P P@ssword1 -C -i /init-db.sql