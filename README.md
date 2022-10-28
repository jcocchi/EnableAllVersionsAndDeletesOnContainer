# Enable AllVersionsAndDeletes change feed mode on an existing container

This is a sample project to enable AllVersionsAndDeletes change feed mode on an existing container.

## Setup
Rename the `appsettings.sample.json` file to `appsettings.json` and fill in the `CosmosConnectionString` with your Azure Cosmos DB for NoSQL account connection string.

## Run the application
Enter `dotnet run <Database Name> <Container Name> <Retention in minutes>` at the command line.

|CLI argument |Expected value |Example |
|-------------|---------------|--------|
|Database Name |The name of an existing Azure Cosmos DB for NoSQL database.|myDatabase |
|Container Name |The name of an existing Azure Cosmos DB for NoSQL container in the specified database.|myContainer |
|Retention in minutes |The number of minutes you want the AllVersionsAndDeletes changes to be retained for. This is the amount of time you want to be able to look back and read changes from. The maximum value is 1440 (24 hours) and the minium value is 1.|60 |
