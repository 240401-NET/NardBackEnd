using Service;
using Models;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Infrastructure;

[ApiController]
[Route("[controller]")]
public class TypeController:ControllerBase
{
    private readonly ITypeService _typeService;
    //private readonly IPokeAPIService _pokeService;
    
    public TypeController(ITypeService typeService)
    {
        _typeService = typeService;
        
    }
    
    
    [HttpPost("makeTypeTable")]
    public async Task<List<Models.Type>> MakeTypeTable()
    {
        List<Models.Type> types = await _typeService.MakeTypeDBTable();
        return types;
    }
    
}