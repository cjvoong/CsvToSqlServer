for i in {1..50};
do
    /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P 3qoLxoCRw7 -d master -i schema.sql
    if [ $? -eq 0 ]
    then
        echo "schema.sql completed"
        break
    else
        echo "not ready yet..."
        sleep 1
    fi
done