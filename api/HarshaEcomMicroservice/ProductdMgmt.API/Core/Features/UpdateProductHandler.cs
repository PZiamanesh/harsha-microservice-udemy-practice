namespace ProductMgmt.API.Core.Features;

public class UpdateProductHandler : IRequestHandler<UpdateProductRequest, ErrorOr<Updated>>
{
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateProductHandler(
        IProductRepository productRepository,
        IUnitOfWork unitOfWork)
    {
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<Updated>> Handle(UpdateProductRequest request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetProductByIdAsync(request.ProductID);

        if (product is null)
        {
            return Error.Conflict(description: "Product not found. Update canceled");
        }

        product.Update(request.ProductName, request.Category, request.UnitPrice, request.QuantityInStock);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Updated;
    }
}