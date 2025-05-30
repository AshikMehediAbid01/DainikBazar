using System.Net.Http;
using System.Text;
using DainikBazar.UI.ApiServices.Interfaces;
using DainikBazar.UI.Models;
using Newtonsoft.Json;

namespace DainikBazar.UI.ApiServices.Implementations;

public class ReviewApiService : IReviewApiService
{
    Uri baseAddress = new Uri("https://localhost:7155/api/");
    private readonly HttpClient _httpClient;

    public ReviewApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = baseAddress;
    }


    public async Task<bool> CreateReviewAsync(ReviewAndRatingVM review)
    {
        string data = JsonConvert.SerializeObject(review);
        StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync(_httpClient.BaseAddress + "ReviewAndRating/CreateReview", content);

        return response.IsSuccessStatusCode;
    }
}
