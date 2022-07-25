namespace GnbApi.Infrastructure.Databases.RatesTransactions;

using Application.Common.Enums;
using Application.Rates;
using Application.Services.RateServices;
using Application.Services.TransactionServices;
using Application.Transactions;
using Application.Transactions.Dto;
using AutoMapper;
using Entities;
using Extensions;
using Microsoft.EntityFrameworkCore;
using ApplicationRate = Application.Rates.Dto.Rates;
using ApplicationTransaction = Application.Transactions.Dto.Transaction;
using Transaction = Entities.Transaction;

internal class EntityFrameworkRateTransactionRepository : IRatesRepository, ITransactionRepository
{
    private readonly GnbDbContext context;
    private readonly IRateServices rateServices;
    private readonly ITransactionServices transactionServices;
    private readonly IMapper mapper;

    public EntityFrameworkRateTransactionRepository(IRateServices rateServices, IMapper mapper, GnbDbContext context, ITransactionServices transactionServices)
    {
        this.rateServices = rateServices;
        this.mapper = mapper;
        this.context = context;
        this.transactionServices = transactionServices;

        if (this.context != null)
        {
            _ = this.context.Database.EnsureDeleted();
            _ = this.context.Database.EnsureCreated();
        }
    }

    #region Rates
    public async Task<List<ApplicationRate>> GetRates(CancellationToken cancellationToken)
    {
        if (!this.context.Rate.Any())
        {
            await this.AddRates(cancellationToken);
        }
        var rates = await this.context.Rate.AsNoTracking().ToListAsync(cancellationToken);
        return this.mapper.Map<List<ApplicationRate>>(rates);
    }

    private async Task AddRates(CancellationToken cancellationToken)
    {
        var rates = await this.rateServices.GetRates(cancellationToken);
        if (rates?.Count > 0 )
        {
            this.context.Rate.AddRange(this.mapper.Map<List<Rates>>(rates));
            await this.context.SaveChangesAsync(cancellationToken);
        }
    }
    #endregion

    #region Transaction
    public async Task<List<ApplicationTransaction>> GetTransactions(CancellationToken cancellationToken)
    {
        if (!this.context.Transaction.Any())
        {
            await this.AddTransaction(cancellationToken);
        }

        var transactions = await this.context.Transaction.AsNoTracking().ToListAsync(cancellationToken);
        return this.mapper.Map<List<ApplicationTransaction>>(transactions);
    }

    public Task<TransactionBySku> GetTransactionBySku(IEnumerable<ApplicationTransaction> transactions, string sku, Currency currency, CancellationToken cancellationToken)
    {
        var filterTransactionsBySku = transactions.Where(tr => tr.Sku == sku && tr.Currency == currency).ToList();
        return Task.FromResult(filterTransactionsBySku.ToTransactionBySku(filterTransactionsBySku, currency));
    }

    public Task<bool> TransactionBySkuExists(IEnumerable<ApplicationTransaction> transactions, string sku, Currency currency)
    {
        return Task.FromResult(transactions.Any(tr => tr.Sku == sku && tr.Currency == currency));
    }


    private async Task AddTransaction(CancellationToken cancellationToken)
    {
        var transactions = await this.transactionServices.GetTransactions(cancellationToken);
        if (transactions?.Count > 0)
        {
            this.context.Transaction.AddRange(this.mapper.Map<List<Transaction>>(transactions));
            await this.context.SaveChangesAsync(cancellationToken);
        }
    }

    #endregion


}
