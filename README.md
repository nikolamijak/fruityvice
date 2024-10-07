Project Structure

The following are the main components of the application:
1. FruitApiClient

The FruitApiClient is a typed HTTP client responsible for making requests to the external API. It contains a single method GetFruitByNameAsync, which fetches fruit information by name.

Key Points:

    Encapsulates the API call logic.
    Handles potential errors such as 404 (Not Found) or API connectivity issues.
    Makes the API calls more maintainable by providing a clear interface to the rest of the application.


2. MediatR Handlers

The application uses MediatR to separate the logic for commands and queries. Each operation (e.g., fetching a fruit, adding metadata) is implemented as a request handled by MediatR, ensuring clean separation of concerns.

Key Handlers:

    GetFruiteRequestHandler: Handles fetching fruit details by name.
    AddMetadataRequestHandler: Handles adding metadata to the fruit.
    UpdateMetadataRequestHandler: Handles updating fruit metadata.
    DeleteMetadataRequestHandler: Handles deleting fruit metadata.

3. Records and Models

Records are used to define the structure of requests and responses. These lightweight, immutable objects make it easy to pass data through the application.

    FruitResponse: Represents the fruit information returned by the external API.
    MetadataRequest: Represents a metadata key-value pair.
    FruitCommand: Wraps the fruit name and metadata in a single object.

4. Minimal API Endpoints

The application defines several HTTP endpoints that interact with the external API and the metadata functionality. These are registered using the MapGroup functionality of .NET 8.0, making it easier to group related routes.
Endpoint Overview:

    GET /fruit/{name}
        Fetches the fruit details by name using the external API.
        Enriches the fruit data with metadata (if present).
        Returns FruitResponse or an error message if the fruit is not found.

    POST /fruit/{name}/metadata
        Adds metadata to a specific fruit.
        The fruit name is extracted from the URL, and metadata is passed in the request body.
        Returns the updated FruitResponse.

    PUT /fruit/{name}/metadata
        Updates metadata for a specific fruit.
        The fruit name is extracted from the URL, and metadata is passed in the request body.
        Returns the updated FruitResponse.

    DELETE /fruit/{name}/metadata
        Deletes metadata for a specific fruit.
        The fruit name is extracted from the URL, and metadata to delete is passed in the request body.
        Returns the updated FruitResponse.

5. Error Handling

    All endpoints use proper HTTP response codes to indicate the outcome of the operation.
    If a fruit is not found (404), the application returns a custom ExceptionResponse.
    In case of an internal server error or invalid input, the application returns an appropriate error response with details.

6. OpenAPI (Swagger) Integration

The API is integrated with OpenAPI/Swagger for automatic generation of API documentation. You can explore the available endpoints and try them out directly via Swagger UI.

To access the Swagger UI, once the API is running, go to:


http://localhost:5005/swagger

Build and Run Instructions
Prerequisites

    .NET 8.0 SDK installed on your machine.
    Visual Studio (at least Community Edition), with the ASP.NET and web development workload.

Running the Project from fruityvice_nikola.zip

    Download and Extract the Archive:

    First, download and extract the fruityvice_nikola.zip archive to a directory on your machine.

    Using Visual Studio (Community Edition or Higher):

    a. Open the Project:
        Launch Visual Studio.
        Go to File -> Open -> Project/Solution.
        Navigate to the extracted folder from fruityvice_nikola.zip and select the solution file fruitvice.sln

    b. Build the Project:
        Once the project is loaded, go to the Solution Explorer.
        Right-click on the project and select Build.

    c. Run the Project:
        Once the build is successful, click the Run or Start Debugging button (green arrow at the top) to run the project.
        The project will run on http://localhost:5005/ by default.

    Using .NET CLI:

    a. Open a Terminal:
        Navigate to the folder where the archive was extracted using the terminal or command prompt.

    b. Restore Packages:
        Run the following command to restore any missing packages:

        bash

    dotnet restore

c. Build the Project:

    Run the following command to build the project:

    dotnet build

d. Run the Project:

    After building, use the following command to run the project:    

        dotnet run

    e. Access the API:
        Once the application starts, it will be available at http://localhost:5005/.

Future Maintenance Notes

    External API Changes:
        If the external API changes (e.g., endpoints, response format), update the FruitApiClient class to reflect these changes.
        Ensure that MediatR handlers correctly handle the new response format or any additional error cases.

    Additional Features:
        You can easily extend this API by adding new endpoints (e.g., searching for fruits by family or genus).
        To implement more complex metadata operations, consider modifying the structure of MetadataRequest and corresponding commands/handlers.

    Logging:
        Add logging to track external API interactions and metadata updates to enhance debugging and supportability.
        Integrate with logging frameworks like Serilog or NLog for more robust logging options.