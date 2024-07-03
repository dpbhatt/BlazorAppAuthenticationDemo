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

    //public async Task<List<TemplateDTO>?> All()
    //{

    //    var templates = await _httpClient.GetAsync($"api/Templates/all");

    //    List<TemplateDTO>? data = null;

    //    if (templates.IsSuccessStatusCode
    //        && !IsIgnorePathExists(templates?.RequestMessage?.RequestUri?.LocalPath ?? ""))
    //    {
    //        data = await templates.Content.ReadFromJsonAsync<List<TemplateDTO>?>();
    //    }

    //    return data;
    //}

    public async Task<List<TemplateDTO>?> All()
    {
        var templates = await _httpClient.GetAsync($"api/Templates/all");

        List<TemplateDTO>? data = null;
        Console.WriteLine($"XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXCheckingXXXXXXXXXXXXXXXX{DateTime.Now}");
        var loginPage = await templates.Content.ReadAsStringAsync();
        if (templates.IsSuccessStatusCode
            && !IsIgnorePathExists(templates?.RequestMessage?.RequestUri?.LocalPath ?? ""))
        {

            try
            {
                data = await templates.Content.ReadFromJsonAsync<List<TemplateDTO>?>();
                Console.WriteLine($"XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXData received: {data?.Count} {DateTime.Now} templatesXXXXXXXXXXXXXXXX");
            }
            catch (Exception e)
            {

                throw;
            }
        }
        else
        {
            Console.WriteLine($"XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXFailedXXXXXXXXXXXXXXXX{DateTime.Now}");
        }
        return data;
    }

    private bool IsIgnorePathExists(string localPath)
    {
        return _omitPaths.Any(omitPath => omitPath.Contains(localPath, StringComparison.OrdinalIgnoreCase));
    }
}