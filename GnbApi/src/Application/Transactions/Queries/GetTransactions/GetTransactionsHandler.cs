namespace GnbApi.Application.Transactions.Queries;

using Dto;
using MediatR;

public class GetTransactionsHandler : IRequestHandler<GetTransactionsQuery, List<Transaction>>
{
    private readonly ITransactionRepository transactionRepository;

    public GetTransactionsHandler(ITransactionRepository transactionRepository)
    {
        this.transactionRepository = transactionRepository;
    }

    public Task<List<Transaction>> Handle(GetTransactionsQuery request, CancellationToken cancellationToken)
    {
        return this.transactionRepository.GetTransactions(cancellationToken);
    }
}
