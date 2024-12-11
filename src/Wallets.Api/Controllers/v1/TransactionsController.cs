using Asp.Versioning;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Wallets.Application.Commons;
using Wallets.Application.UseCases.ProcessTransaction;
using Wallets.Application.UseCases.Shared;

namespace Wallets.Api.Controllers.v1;

[ApiController]
[ApiVersion(version: "1")]
[Route(template: "v{version:apiVersion}/transactions")]
public class TransactionsController(
     IProcessTransactionUseCase processTransactionUseCase) : ControllerBase
{
     [HttpPost()]
     [ProducesResponseType(
          type: typeof(ProcessTransactionResponse), 
          statusCode: StatusCodes.Status201Created)]
     [ProducesResponseType(statusCode: StatusCodes.Status409Conflict, type: typeof(void))]
     [ProducesResponseType(statusCode: StatusCodes.Status400BadRequest, type: typeof(List<string>))]
     public async Task<IActionResult> PostAsync(
         [FromBody] ProcessTransactionModel processTransactionModel,
         CancellationToken cancellationToken)
     {
          var request = ProcessTransactionRequest.Factory(
               payerId: processTransactionModel.PayerId,
               receiverId: processTransactionModel.ReceiverId,
               amount: processTransactionModel.Amount,
               transactionDate: processTransactionModel.TransactionDate);

          var result = await processTransactionUseCase.HandleAsync(
               request, 
               cancellationToken);

          return result.Type switch
          {
               Response<ProcessTransactionResponse>.ResponseType.Conflict
                   => Conflict(),
               Response<ProcessTransactionResponse>.ResponseType.ValidationError
                   => BadRequest(result.Errors),
               _ => Created(string.Empty, result.Content),
          };
     }
}

