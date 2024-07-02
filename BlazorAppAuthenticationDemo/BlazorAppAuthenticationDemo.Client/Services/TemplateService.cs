using BlazorAppAuthenticationDemo.Shared.Models;
using System.Net.Http.Json;

namespace BlazorAppAuthenticationDemo.Client.Services;

public class TemplateService(HttpClient httpClient)
{
    private readonly HttpClient _httpClient = httpClient;

    public async Task<List<TemplateDTO>?> All()
    {
        var templates = await _httpClient.GetAsync($"api/Templates/all");

        List<TemplateDTO>? data = null;

        if (templates.IsSuccessStatusCode)
        {
            data = await templates.Content.ReadFromJsonAsync<List<TemplateDTO>?>();
        }
        return data;
    }
}