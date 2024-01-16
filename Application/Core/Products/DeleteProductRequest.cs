using Application.Common.Exceptions;

namespace Application.Core.Products
{
    public class DeleteProductRequest : IRequest<int>
    {
        public int Id { get; set; }
    }

    public class DeleteProductRequestHandler : IRequestHandler<DeleteProductRequest, int>
    {
        private readonly IProductService _productService;

        public DeleteProductRequestHandler(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<int> Handle(DeleteProductRequest request, CancellationToken cancellationToken)
        {
            var product = await _productService.GetByIdAsync(request.Id);

            if (product == null)
            {
                throw new NotFoundException("Product not found");
            }

            await _productService.DeleteAsync(request.Id);
            return request.Id;
        }
    }
}
