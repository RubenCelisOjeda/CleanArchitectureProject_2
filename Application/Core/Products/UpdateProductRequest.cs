using Application.Common.Exceptions;
using Domain;

namespace Application.Core.Products
{
    public class UpdateProductRequest : IRequest<int>
    {
        public int Id { get; set; }
        public Product UpdatedProduct { get; set; }
    }

    public class UpdateProductRequestHandler : IRequestHandler<UpdateProductRequest, int>
    {
        private readonly IProductService _productService;

        public UpdateProductRequestHandler(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<int> Handle(UpdateProductRequest request, CancellationToken cancellationToken)
        {
            var product = await _productService.GetByIdAsync(request.Id);

            if (product == null)
            {
                throw new NotFoundException("Product not found");
            }

            await _productService.UpdateAsync(request.UpdatedProduct);
            return request.Id;
        }
    }
}
