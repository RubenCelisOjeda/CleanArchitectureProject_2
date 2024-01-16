using Domain;

namespace Application.Core.Products
{
    public class GetProductsRequest : IRequest<IEnumerable<Product>>
    {
    }

    public class GetProductsRequestHandler : IRequestHandler<GetProductsRequest, IEnumerable<Product>>
    {
        private readonly IProductService _productService;

        public GetProductsRequestHandler(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<IEnumerable<Product>> Handle(GetProductsRequest request, CancellationToken cancellationToken)
        {
            return await _productService.GetAllAsync();
        }
    }
}
