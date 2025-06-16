using System.Text;
using DainikBazar.UI.ApiServices.Interfaces;
using DainikBazar.UI.Models;
using Newtonsoft.Json;

namespace DainikBazar.UI.ApiServices.Implementations;

public class ProductApiService : IProductApiService
{
    Uri baseAddress = new Uri("https://localhost:7155/api/");
    private readonly HttpClient _httpClient;

    public ProductApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = baseAddress;
    }




    public async Task<bool> CreateProductAsync(ProductVM product)
    {
        string data = JsonConvert.SerializeObject(product);
        StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync(_httpClient.BaseAddress + "Products/CreateProduct", content);

        return response.IsSuccessStatusCode;
    }



    public async Task<List<ProductVM>> GetAllProductsAsync()
    {
        List<ProductVM> products = new List<ProductVM>();
        var response = await _httpClient.GetAsync(_httpClient.BaseAddress + "Products/GetAllProducts");

        if (response.IsSuccessStatusCode)
        {
            var json = response.Content.ReadAsStringAsync().Result;
            products = JsonConvert.DeserializeObject<List<ProductVM>>(json) ?? new List<ProductVM>();
        }

        return products;
    }



    public async Task<ProductVM?> GetProductByIdAsync(string id)
    {
        var response = _httpClient.GetAsync(_httpClient.BaseAddress + "Products/GetProduct/" + id).Result;

        if (response.IsSuccessStatusCode)
        {
            var json = await response.Content.ReadAsStringAsync();
            var product = JsonConvert.DeserializeObject<ProductVM>(json);
            return product;
        }

        return null;

    }




    public async Task<bool> UpdateProductAsync(ProductVM product)
    {
        string data = JsonConvert.SerializeObject(product);
        StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
        var response = await _httpClient.PutAsync(_httpClient.BaseAddress + "Products/UpdateProduct", content);

        return response.IsSuccessStatusCode;
    }



    public async Task<bool> DeleteProductAsync(int id)
    {
        HttpResponseMessage response = await _httpClient.DeleteAsync(_httpClient.BaseAddress + "Products/DeleteProduct/" + id);

        return response.IsSuccessStatusCode;
    }


}
