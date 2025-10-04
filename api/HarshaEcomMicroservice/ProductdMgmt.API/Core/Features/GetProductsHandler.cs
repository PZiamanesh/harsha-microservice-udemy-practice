namespace ProductMgmt.API.Core.Features;

public class GetProductsHandler : IRequestHandler<GetProductsFilter, ErrorOr<List<ProductResponse>>>
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public GetProductsHandler(
        IProductRepository productRepository,
        IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<ErrorOr<List<ProductResponse>>> Handle(
        GetProductsFilter filter,
        CancellationToken cancellationToken)
    {
        var products = await _productRepository.GetProductsAsync(filter);
        return _mapper.Map<List<ProductResponse>>(products);
    }
}
