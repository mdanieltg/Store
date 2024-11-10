using System.Net.Mime;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StoreWebAPI.DataTransferObjects;

namespace StoreWebAPI.Controllers;

[ApiController]
[Route("/api/products")]
[Produces(MediaTypeNames.Application.Json)]
[Authorize(Roles = "Customer,Seller")]
public class ProductsController : ControllerBase
{
    [HttpGet("{productId:guid}")]
    [Authorize(Roles = "Customer,Seller")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async ValueTask<ActionResult<ProductDto>> GetProduct([FromRoute] Guid productId)
    {
        throw new NotImplementedException();
    }
}
