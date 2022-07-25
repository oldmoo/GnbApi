namespace GnbApi.Application.Transactions.Queries;

using Dto;
using MediatR;

public class GetTransactionsQuery : IRequest<List<Transaction>>
{

}
