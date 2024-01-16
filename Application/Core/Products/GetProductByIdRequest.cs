using Application.Common.Exceptions;
using Domain;

namespace Application.Core.Products
{
    public class GetProductByIdRequest : IRequest<Product>
    {
        public int Id { get; set; }
    }

    public class GetProductByIdRequestHandler : IRequestHandler<GetProductByIdRequest, Product>
    {
        private readonly IProductService _productService;

        public GetProductByIdRequestHandler(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<Product> Handle(GetProductByIdRequest request, CancellationToken cancellationToken)
        {
            var product = await _productService.GetByIdAsync(request.Id);

            if (product == null)
            {
                throw new NotFoundException("Product not found");
            }

            return product;
        }
    }
}
