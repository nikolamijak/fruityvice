using Fruityvice.Api.Application.Models.Contracts;
using Fruityvice.Api.Application.Ports;
using Fruityvice.Api.Application.Exceptions;

namespace Fruityvice.Api.Infrastructure.Http;

public class FruitApiClient(HttpClient httpClient) : IFruitApiService
{
       
    public async Task<Fruit> GetFruitByNameAsync(string name)
    {
        try
        {
            var response = await httpClient.GetAsync($"/api/fruit/{name}");

            if (response.IsSuccessStatusCode)
            {
                var fruit = await response.Content.ReadFromJsonAsync<Fruit>();
                if (fruit != null)
                {
                    return fruit;
                }
                else
                {
                    throw new ApiIntegrationException("Invalid response format from API.");                    
                }
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                var errorResponse = await response.Content.ReadFromJsonAsync<ApiError>();
                var failureMsg = errorResponse?.Error != null ? $"{name} {errorResponse?.Error}" : $"{name} not found.";
                throw new NotFoundException(failureMsg);
            }
            else
            {
                throw new ApiIntegrationException($"Unexpected status code: {response.StatusCode}", (int) response.StatusCode);
            }
        }
        catch (HttpRequestException ex)
        {
           
            throw new ApiIntegrationException("Error connecting to the external API.", ex);
        }
        catch (Exception ex)
        {
            throw ex switch
            {
                _ when ex is not NotFoundException && ex is not ApiIntegrationException =>  new ApiIntegrationException("An unexpected error occurred.", ex),
                _ =>  ex
            };            
        }
    }
}
