# Persistence Project

## EF CLI

```powershell
# add new migration
dotnet ef migrations add initialMigration -s Synker.Api -c SynkerDbContext -p Synker.Persistence --no-build
# remove migration
dotnet ef migrations remove -s Synker.Api -c SynkerDbContext -p Synker.Persistence -v
# update database schema
dotnet ef database update -s Synker.Api -c SynkerDbContext -p Synker.Persistence -v
```