using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

public class ItemService
{
    private readonly HttpClient _httpClient;

    public ItemService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    // Método para obtener la lista de ítems desde la API
    
    public async Task<List<Item>> GetItemsAsync()
{
    return await _httpClient.GetFromJsonAsync<List<Item>>("api/items") 
        ?? new List<Item>(); // Manejo de null: retorna una lista vacía si no se obtiene nada
}

    // Método para obtener los detalles de un ítem específico
    public async Task<Item?> GetItemDetailAsync(int id)
    {
        return await _httpClient.GetFromJsonAsync<Item>($"api/items/{id}");
    }
}
