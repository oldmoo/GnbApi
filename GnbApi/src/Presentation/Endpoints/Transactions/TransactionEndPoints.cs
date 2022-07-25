namespace GnbApi.Presentation.Endpoints.Transactions;

using System.Net.Mime;
using Application.Common.Enums;
using Application.Transactions.Dto;
using Application.Transactions.Queries;
using Application.Transactions.Queries.GetTransactionsBySku;
using Errors;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

public static class TransactionEndPoint
{
    public static WebApplication MapTransactionsEndpoints(this WebApplication app)
    {
        _ = app.MapGet("/api/transactions", GetTransactions)
            .WithTags("Transactions")
            .WithMetadata(new SwaggerOperationAttribute("Lookup all Transactions", "\n    GET /Transactions"))
            .Produces<List<Transaction>>(StatusCodes.Status200OK, contentType: MediaTypeNames.Application.Json)
            .Produces<ApiError>(StatusCodes.Status500InternalServerError, contentType: MediaTypeNames.Application.Json);

        _ = app.MapGet("/api/transactions/{sku}/{currency}", GetTransactionsBySku)
            .WithTags("TransactionsBySku")
            .WithMetadata(new SwaggerOperationAttribute("Lookup Transactions by Sku in Euro", "\n    GET /Transactions by Sku in Euro /00000000-0000-0000-0000-000000000000"))
            .Produces<List<TransactionBySku>>(StatusCodes.Status200OK, contentType: MediaTypeNames.Application.Json)
            .Produces<ApiError>(StatusCodes.Status400BadRequest, contentType: MediaTypeNames.Application.Json)
            .Produces<ApiError>(StatusCodes.Status404NotFound, contentType: MediaTypeNames.Application.Json)
            .Produces<ApiError>(StatusCodes.Status500InternalServerError, contentType: MediaTypeNames.Application.Json);
        return app;
    }

    private static async Task<IResult> GetTransactions(IMediator mediator)
    {
        return Results.Ok(await mediator.Send(new GetTransactionsQuery()));
    }

    private static async Task<IResult> GetTransactionsBySku(IMediator mediator, [FromRoute] string sku, [FromRoute] string currency)
    {
        return Results.Ok(await mediator.Send(new GetTransactionBySkuQuery() { Sku = sku, Currency = currency }));
    }
}
