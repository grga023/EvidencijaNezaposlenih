#!/bin/bash
# test
# Start SQL Server in the background
/opt/mssql/bin/sqlservr &

# Wait for SQL Server to be ready to accept connections (connect to master)
echo "Waiting for SQL Server to start..."
until /opt/mssql-tools18/bin/sqlcmd -S localhost -U SA -P "YourPassword123!" -d master -Q "SELECT 1" -C 2>/dev/null
do
  echo "⏳ Waiting for SQL Server to start..."
  sleep 1
done

# Get list of databases into a variable, trim spaces
DB_LIST=$(/opt/mssql-tools18/bin/sqlcmd -S localhost -U SA -P "YourPassword123!" -d master -Q "SET NOCOUNT ON; SELECT RTRIM(LTRIM(name)) FROM sys.databases;" -h -1 -C | sed '/^$/d')

# Check if 'Identiteti' database exists
if echo "$DB_LIST" | grep -qw "Identiteti"; then
  echo "✅ Database 'Identiteti' exists."
else
  echo "❌ Database 'Identiteti' NOT found."
fi

# Check if 'EvidencijaNezaposlenih' database exists
if echo "$DB_LIST" | grep -qw "EvidencijaNezaposlenih"; then
  echo "✅ Database 'EvidencijaNezaposlenih' exists."
else
  echo "❌ Database 'EvidencijaNezaposlenih' NOT found."
fi

  echo "⏳ Waiting for SQL Server to start..."
  sleep 10

# Only try to insert admin user if 'Identiteti' exists
if echo "$DB_LIST" | grep -qw "Identiteti"; then
  /opt/mssql-tools18/bin/sqlcmd -S localhost -U SA -P "YourPassword123!" -d Identiteti -Q "
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
      [Id], [Ime], [Prezime], [Adresa], [Kreiran], [UserName], [NormalizedUserName], [Email], 
      [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], 
      [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount])
    VALUES (
      @adminUserId, 'Admin', 'User', 'Admin Address', SYSDATETIME(), 'admin', 'ADMIN', 'admin@example.com',
      'ADMIN@EXAMPLE.COM', 1, 'AQAAAAIAAYagAAAAENBXwye6QT1KeQPwuEqxG9KpZLOAU7UUisnCRyRB00SGHnueVZSVRB0DJ+peGtpNpA==', NEWID(), NEWID(), NULL, 0, 0, NULL, 1, 0);

    SELECT @adminRoleId = Id FROM [dbo].[AspNetRoles] WHERE Name = 'admin';

    IF @adminRoleId IS NOT NULL
    BEGIN
      INSERT INTO [dbo].[AspNetUserRoles] (UserId, RoleId) VALUES (@adminUserId, @adminRoleId);
    END
  END
  " -b -C

  echo "✅ - DONE"

else
  echo "Skipping admin user insert because database 'Identiteti' does not exist."
fi

echo "🚀 SQL Server is up!"

# Keep container alive (optional)
wait
