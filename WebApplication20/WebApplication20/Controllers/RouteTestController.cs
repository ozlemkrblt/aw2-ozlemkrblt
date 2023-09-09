using Microsoft.AspNetCore.Mvc;

namespace WebApplication20.Controllers;


[Route("api/[controller]")]
[ApiController]
public class RouteTestController : ControllerBase
{
    public RouteTestController()
    {

    }

    [HttpGet("GetFromQuery")]
    public int GetQuery([FromQuery] int id)
    {
        return id;
    }
    [HttpGet("GetFromRoute/{id}")]
    public int GetRoute(int id)
    {
        return id;
    }


    [HttpGet("GetFromQueryMulti")]
    public string GetFromQueryMulti([FromQuery] int? id, [FromQuery] int? lat, [FromQuery] int? lng)
    {
        return $"{id}-{lat}-{lng}";
    }
    [HttpGet("GetFromRouteMulti/{id}/{lat}/{lng}")]
    public string GetRoute(int? id,int? lat,int? lng)
    {
        return $"{id}-{lat}-{lng}";
    }


    [HttpGet("GetFromRouteMultiSelect/{id}")]
    public string GetRoute(int id, [FromQuery] int? lat, [FromQuery] int? lng)
    {
        return $"{id}-{lat}-{lng}";
    }

}
