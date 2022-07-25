namespace GnbApi.Application.Services.TransactionServices;

using Common.Enums;
using Common.Exceptions;
using Newtonsoft.Json;
using Transactions.Dto;

public class TransactionService : ITransactionServices
{
    private readonly IHttpClientFactory httpClientFactory;

    public TransactionService(IHttpClientFactory httpClientFactory)
    {
        this.httpClientFactory = httpClientFactory;
    }

    public async Task<List<Transaction>> GetTransactions(CancellationToken cancellationToken)
    {
        using var client = this.httpClientFactory.CreateClient("TransactionApi");
        using var response = await client.GetAsync($"{client.BaseAddress}", cancellationToken);
        if (!response.IsSuccessStatusCode )
        {
            NotFoundException.Throw(EntityType.Transaction);
        }
        var responseBody = await response.Content.ReadAsStringAsync(cancellationToken);
        var transactions = JsonConvert.DeserializeObject<List<Transaction>>(responseBody);
        return transactions;
    }
}
