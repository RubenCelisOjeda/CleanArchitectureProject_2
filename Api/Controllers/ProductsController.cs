using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Application.Core.Products;
using Domain;

namespace Api.Controllers
{
    [Authorize("BasicUserPermissionsPolicy")]
    [Route("{slugTenant}/[controller]")]
    public class ProductsController : BaseApiController
    {
        [HttpGet]
        public async Task<IEnumerable<Product>> Get()
        {
            return await Mediator.Send(new GetProductsRequest());
        }

        [HttpGet("{id}")]
        public async Task<Product> Get(int id)
        {
            return await Mediator.Send(new GetProductByIdRequest { Id = id });
        }

        [HttpPost]
        public async Task<int> Post([FromBody] Product product)
        {
            return await Mediator.Send(new CreateProductRequest { Product = product });
        }

        [HttpPut("{id}")]
        public async Task<int> Put(int id, [FromBody] Product updatedProduct)
        {
            return await Mediator.Send(new UpdateProductRequest { Id = id, UpdatedProduct = updatedProduct });
        }

        [HttpDelete("{id}")]
        public async Task<int> Delete(int id)
        {
            return await Mediator.Send(new DeleteProductRequest { Id = id });
        }
    }
}


