#!/bin/bash
set -e

#######################################
# Start SQL Server in background
#######################################
/opt/mssql/bin/sqlservr &

SQL_PID=$!

# Ensure SQL Server is stopped cleanly
trap "kill $SQL_PID; wait $SQL_PID" SIGTERM SIGINT

echo "⏳ Waiting for SQL Server to start..."

until /opt/mssql-tools18/bin/sqlcmd -S localhost -U SA -P "YourPassword123!" -d master -Q "SELECT 1" -C >/dev/null 2>&1
do
  echo "⏳ Waiting for SQL Server to start..."

  sleep 1
done

echo "✅ SQL Server is ready"

#######################################
# Wait until databases exist and are ONLINE
#######################################
wait_for_db() {
  local db_name=$1
  until /opt/mssql-tools18/bin/sqlcmd \
    -S localhost \
    -U SA \
    -P "YourPassword123!" \
    -d master \
    -Q "SELECT state_desc FROM sys.databases WHERE name='$db_name'" \
    -h -1 \
    -C | grep -q "ONLINE"; do
    sleep 1
  done
}

wait_for_db "Identiteti"
wait_for_db "EvidencijaNezaposlenih"

echo "✅ Databases are online"

#######################################
# Insert admin user into Identiteti DB Admin123!
#######################################
echo "👤 Ensuring admin user exists..."

/opt/mssql-tools18/bin/sqlcmd \
  -S localhost \
  -U SA \
  -P "YourPassword123!" \
  -d master \
  -b \
  -C \
  -Q "
IF DB_ID('Identiteti') IS NOT NULL
BEGIN
    USE Identiteti;

    SET ANSI_NULLS ON;
    SET ANSI_WARNINGS ON;
    SET CONCAT_NULL_YIELDS_NULL ON;
    SET ANSI_PADDING ON;
    SET QUOTED_IDENTIFIER ON;

    IF NOT EXISTS (SELECT 1 FROM [dbo].[AspNetUsers] WHERE Ime = 'Admin')
    BEGIN
        DECLARE @adminUserId uniqueidentifier = NEWID();
        DECLARE @adminRoleId uniqueidentifier;

        INSERT INTO [dbo].[AspNetUsers] (
            [Id], [Ime], [Prezime], [Adresa], [Kreiran],
            [UserName], [NormalizedUserName],
            [Email], [NormalizedEmail], [EmailConfirmed],
            [PasswordHash], [SecurityStamp], [ConcurrencyStamp],
            [PhoneNumber], [PhoneNumberConfirmed],
            [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled],
            [AccessFailedCount]
        )
        VALUES (
            @adminUserId, 'Admin', 'User', 'Admin Address', SYSDATETIME(),
            'admin', 'ADMIN',
            'admin@example.com', 'ADMIN@EXAMPLE.COM', 1,
            'AQAAAAIAAYagAAAAEEnkyh5JbH3smtwgp+0eJwaC9jzBJMksn0WdYRij0QCHTd7ncNu5zUdEgMW8PTGnSw==',
            NEWID(), NEWID(),
            NULL, 0, 0, NULL, 1, 0
        );

        SELECT @adminRoleId = Id FROM [dbo].[AspNetRoles] WHERE Name = 'admin';

        IF @adminRoleId IS NOT NULL
        BEGIN
            INSERT INTO [dbo].[AspNetUserRoles] (UserId, RoleId)
            VALUES (@adminUserId, @adminRoleId);
        END
    END
END
"

echo "✅ Admin user ensured"

echo "🚀 SQL Server is running"

# Keep container alive
wait $SQL_PID
