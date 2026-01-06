
using Basket.API.Data;

namespace Basket.API.Basket.GetBasket
{
    public record GetBasketQuery(string uerName):IQuery<GetBasketResult>;
    public record GetBasketResult(ShoppingCart ShoppingCart);
    public class GetBasketQueryHandler(IBasketRepository repository) : IQueryHandler<GetBasketQuery, GetBasketResult>
    {
        public async Task<GetBasketResult> Handle(GetBasketQuery query, CancellationToken cancellationToken)
        {
            var result =await repository.GetBasket(query.uerName, cancellationToken);
            return new GetBasketResult(result);
        }
    }
}
