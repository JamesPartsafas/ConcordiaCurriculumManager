#!/bin/bash

# Arguments
DATABASE_CONNECTION_STRING=$1
DAYS_OLD=$2

# Construct the PostgreSQL command
PG_COMMAND="DELETE FROM public.\"ProdLogs\" WHERE \"TimeStamp\" < NOW() - INTERVAL '$DAYS_OLD days';"

# Execute the command
psql -c "$PG_COMMAND" -v ON_ERROR_STOP=1 -X "$DATABASE_CONNECTION_STRING"

retVal=$?
if [[ $retVal -ne 0 ]]; then
    echo "Old log entries has not been deleted. Failed."
    exit $retVal
else
    echo "Old log entries have been successfully cleaned up."
fi