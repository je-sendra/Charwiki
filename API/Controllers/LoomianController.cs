using Microsoft.AspNetCore.Mvc;

namespace VewTech.Charwiki.API;

[Route("[controller]")]
public class LoomianController : Controller
{
    [HttpGet("test")]
    public string Test()
    {
        return "HOLA";
    }
}