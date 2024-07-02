using BlazorAppAuthenticationDemo.Shared.Models;
using System.Net.Http.Json;

namespace BlazorAppAuthenticationDemo.Client.Services;

public class TemplateService
{
    private readonly HttpClient _httpClient;

    private readonly string[] _omitPaths = ["/Account/Login", "/Account/AccessDenied"];
    public TemplateService(IHttpClientFactory factory)
    {
        _httpClient = factory.CreateClient("serverApi");
    }

    public async Task<List<TemplateDTO>?> All()
    {

        var templates = await _httpClient.GetAsync($"api/Templates/all");

        List<TemplateDTO>? data = null;

        if (templates.IsSuccessStatusCode
            && !IsIgnorePathExists(templates?.RequestMessage?.RequestUri?.LocalPath ?? ""))
        {
            data = await templates.Content.ReadFromJsonAsync<List<TemplateDTO>?>();
        }

        return data;
    }

    private bool IsIgnorePathExists(string localPath)
    {
        return _omitPaths.Any(omitPath => omitPath.Contains(localPath, StringComparison.OrdinalIgnoreCase));
    }
}