# Enable AllVersionsAndDeletes change feed mode on an existing container

This sample project enables AllVersionsAndDeletes change feed mode on an existing container. It is for accounts enrolled in the private preview of this feature who don't have [continuous backups with point in time restore](https://learn.microsoft.com/en-us/azure/cosmos-db/continuous-backup-restore-introduction) enabled.

> Note: If you are using the preview of AllVersionsAndDeletes change feed mode on an account with continuous backups enabled, the retention period is equal to the backup window configured on the account. No further action is needed in this scenario, and attempting to use this sample will throw an error.

## Setup
Rename the `appsettings.sample.json` file to `appsettings.json` and fill in the `CosmosConnectionString` with your Azure Cosmos DB for NoSQL account connection string.

## Run the application
From the command line, navigate to the root directory of the project. Then enter `dotnet run <Database Name> <Container Name> <Retention in minutes>` at the command line. If you don't specify a value for retention, the maximum value of 1440 minutes will be used as default.

```cmd
cd EnableAllVersionsAndDeletesOnContainer
dotnet run myDatabase myContainer 60
```

|CLI argument |Expected value |Example |
|-------------|---------------|--------|
|Database Name |The name of an existing Azure Cosmos DB for NoSQL database.|myDatabase |
|Container Name |The name of an existing Azure Cosmos DB for NoSQL container in the specified database.|myContainer |
|(Optional) Retention in minutes |The number of minutes you want the AllVersionsAndDeletes changes to be retained for. This is the amount of time you want to be able to look back and read changes from. The maximum value is 1440 (24 hours) and the minium value is 1. When not set, 1440 is used as default.|60 |
