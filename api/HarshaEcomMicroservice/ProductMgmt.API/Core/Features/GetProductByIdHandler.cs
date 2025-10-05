namespace ProductMgmt.API.Core.Features;

public class GetProductByIdHandler : IRequestHandler<GetProductByIdRequest, ErrorOr<ProductResponse>>
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public GetProductByIdHandler(
        IProductRepository productRepository,
        IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<ErrorOr<ProductResponse>> Handle(
        GetProductByIdRequest request,
        CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetProductByIdAsync(request.ProductID);

        if (product == null)
        {
            return Error.NotFound(description: $"Product with id: {request.ProductID} was not found.");
        }

        return _mapper.Map<ProductResponse>(product);
    }
}
