using BlazorAppAuthenticationDemo.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlazorAppAuthenticationDemo.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TemplatesController : ControllerBase
{
    private readonly List<TemplateDTO> _templates = [
        new TemplateDTO {Id =1, Name = "Test 1", Content = "This is first template." },
        new TemplateDTO { Id =2, Name = "Test 2", Content = "This is second template."},
        new TemplateDTO { Id =3, Name = "Test 3", Content = "This is third template."}
        ];

    [HttpGet("all")]
    [Authorize(Roles = "Administrators")]
    public async Task<ActionResult<List<TemplateDTO>>> All()
    {
        return Ok(await Task.FromResult(_templates));
    }

    [HttpGet("GetById/{id:int}")]
    [Authorize(Roles = "Users")]
    public async Task<ActionResult<List<TemplateDTO>>> GetById(int id)
    {
        var data = _templates.Find(x => x.Id == id);
        return Ok(await Task.FromResult(data));
    }

    [HttpGet("Anyone")]
    public async Task<ActionResult<string>> Anyone()
    {
        return Ok(await Task.FromResult("Anyone can call this endpoint"));
    }
}