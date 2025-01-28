using System.ComponentModel; 
using ContosoSuitesWebAPI.Entities;
using Microsoft.Azure.Cosmos;
// Exercise 5 Task 2 TODO #5: Add a library references support Semantic Kernel.

namespace ContosoSuitesWebAPI.Plugins
{
    /// <summary>
    /// The maintenance request plugin for creating and saving maintenance requests.
    /// </summary>
    public class MaintenanceRequestPlugin(CosmosClient cosmosClient)
    {
        private readonly CosmosClient _cosmosClient = cosmosClient;

        // Exercise 5 Task 2 TODO #6: Add KernelFunction and Description descriptors to the function.
        // The function should be named "create_maintenance_request" and it should have a description
        // the accurately describes the purpose of the function, such as "Creates a new maintenance request for a Salon."

        // Exercise 5 Task 2 TODO #7: Add Kernel as the first parameter to the function.
        /// <summary>
        /// Creates a new maintenance request for a Salon.
        /// </summary>
        public async Task<MaintenanceRequest> CreateMaintenanceRequest(int SalonId, string Salon, string Details, int? RoomNumber, string? location)
        {
            try
            {
                Console.WriteLine($"Creating a new maintenance request for the {Salon}.");

                var request = new MaintenanceRequest
                {
                    id = Guid.NewGuid().ToString(),
                    Salon_id = SalonId,
                    Salon = Salon,
                    details = Details,
                    room_number = RoomNumber,
                    source = "customer",
                    location = location
                };
                return request;
            }
            catch (Exception ex)
            {
                throw new Exception($"An exception occurred while generating a new maintenance request: {ex}");
            }
        }

        // Exercise 5 Task 2 TODO #8: Add KernelFunction and Description descriptors to the function.
        // The function should be named "save_maintenance_request" and it should have a description
        // the accurately describes the purpose of the function, such as "Saves a maintenance request to the database for a Salon."

        // Exercise 5 Task 2 TODO #9: Add Kernel as the first parameter to the function.
        /// <summary>
        /// Saves a maintenance request to the database for a Salon.
        /// </summary>
        public async Task SaveMaintenanceRequest(MaintenanceRequest maintenanceRequest)
        {
            var db = _cosmosClient.GetDatabase("ContosoSuites");
            var container = db.GetContainer("MaintenanceRequests");

            var response = await container.CreateItemAsync(maintenanceRequest, new PartitionKey(maintenanceRequest.Salon_id));
        }
    }
}
