using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Configuration;

var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

// Validate input
var connectionString = config.GetConnectionString("CosmosConnectionString");
ValidateInput(connectionString, "Please provide a connection string in appsettings.json");

if (args.Length != 3)
{
    throw new ArgumentException("Please enter database name as the first command line argument, container name as the second argument, and retention period as the third argument. Retention period should be an integer representing a number of minutes. Maximum retention is 1440 (24 hours) and minimum is 1.");
}

var dbName = args[0];
ValidateInput(dbName, "Please enter a database name as the first command line argument.");
var containerName = args[1];
ValidateInput(containerName, "Please enter a container name as the second command line argument.");
var retention = ValidateRetention(args[2], "Please enter an integer for the number of minutes in the retention period as the third command line argument. Maximum retention is 1440 (24 hours) and minimum is 1.");

Console.WriteLine($"Setting change feed retention period of {retention} minutes on \n\tdatabase: {dbName} \n\tcontainer: {containerName}");

try
{
    // Get current container config
    var client = new CosmosClient(connectionString);
    var container = client.GetDatabase(dbName).GetContainer(containerName);
    var containerConfig = await container.ReadContainerAsync();
    var containerProps = containerConfig.Resource;

    // Set new retention policy for container
    containerProps.ChangeFeedPolicy.FullFidelityRetention = TimeSpan.FromMinutes(retention);
    await container.ReplaceContainerAsync(containerProps);

    Console.WriteLine($"Change feed retention period successfully set.");
} 
catch (CosmosException e)
{
    if (e.StatusCode == System.Net.HttpStatusCode.NotFound)
    {
        throw new Exception("The database or container doesn't exist. Please pass the name of an existing database as the first command line arguemnt and the name of an existing container as the second command line argument.");
    }
}

// Helper functions
void ValidateInput(string input, string message){
    if (input == null || input == "")
    {
        throw new Exception(message);
    }
}

double ValidateRetention(string input, string message)
{
    if (double.TryParse(input, out double retention))
    {
        if (retention > 0 && retention <= 1440)
        {
            return retention;
        }
    }

    throw new Exception(message);
}
