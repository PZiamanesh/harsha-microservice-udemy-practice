namespace OrderMgmt.API.Core.Features;

public class GetProductsHandler : IRequestHandler<GetProductsRequest, ErrorOr<List<ProductResponse>>>
{
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;

    public GetProductsHandler(
        IProductRepository productRepository,
        IUnitOfWork uow,
        IMapper mapper)
    {
        _productRepository = productRepository;
        _uow = uow;
        _mapper = mapper;
    }

    public async Task<ErrorOr<List<ProductResponse>>> Handle(
        GetProductsRequest request,
        CancellationToken cancellationToken)
    {
        var products = await _productRepository.GetProductsAsync();
        return _mapper.Map<List<ProductResponse>>(products);
    }
}
