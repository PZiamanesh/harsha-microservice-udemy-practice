namespace ProductMgmt.API.Core.Features;

public class UpdateProductHandler : IRequestHandler<UpdateProductRequest, ErrorOr<Updated>>
{
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRabbitMqProducer _rabbitMqProducer;
    private readonly IMapper _mapper;

    public UpdateProductHandler(
        IProductRepository productRepository,
        IUnitOfWork unitOfWork,
        IRabbitMqProducer rabbitMqProducer,
        IMapper mapper)
    {
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
        _rabbitMqProducer = rabbitMqProducer;
        _mapper = mapper;
    }

    public async Task<ErrorOr<Updated>> Handle(
        UpdateProductRequest request,
        CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetProductByIdAsync(request.ProductID);

        if (product is null)
        {
            return Error.NotFound(description: $"Product with id: {request.ProductID} was not found. Update canceled.");
        }

        product.Update(
            request.ProductName,
            request.Category,
            request.UnitPrice,
            request.QuantityInStock);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var productUpdateMsg = _mapper.Map<UpdateProductMessage>(product);
        await _rabbitMqProducer.PublishAsync("product.update", productUpdateMsg);

        return Result.Updated;
    }
}