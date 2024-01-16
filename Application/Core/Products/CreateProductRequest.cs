using Domain;

namespace Application.Core.Products
{
    public class CreateProductRequest : IRequest<int>
    {
        public Product Product { get; set; }
    }

    public class CreateProductRequestHandler : IRequestHandler<CreateProductRequest, int>
    {
        private readonly IProductService _productService;

        public CreateProductRequestHandler(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<int> Handle(CreateProductRequest request, CancellationToken cancellationToken)
        {
            return await _productService.CreateAsync(request.Product);
        }
    }
}
