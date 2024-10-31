using System.Net;

using Asp.Versioning;

using Microsoft.AspNetCore.Mvc;

using Wallets.Application.Commons;
using Wallets.Application.UseCases.CreateWallet;
using Wallets.Application.UseCases.DepositWalletBalance;
using Wallets.Application.UseCases.FindWalletById;
using Wallets.Application.UseCases.Shared;

namespace Wallets.Api.Controllers.v1;

[ApiController]
[ApiVersion("1")]
[Route("v{version:apiVersion}/wallets")]
public class WalletsController(
     ICreateWalletUseCase createWalletUseCase,
     IFindWalletByIdUseCase findWalletByIdUseCase,
     IDepositWalletBalanceUseCase depositWalletBalanceUseCase) : ControllerBase
{
     [HttpPost()]
     [ProducesResponseType(type: typeof(CreateWalletResponse), statusCode: StatusCodes.Status201Created)]
     [ProducesResponseType(statusCode: StatusCodes.Status409Conflict, type: typeof(void))]
     [ProducesResponseType(statusCode: StatusCodes.Status400BadRequest, type: typeof(List<string>))]
     public async Task<IActionResult> PostAsync(
         [FromBody] CreateWalletRequest request,
         CancellationToken cancellationToken)
     {
          var result = await createWalletUseCase.HandleAsync(
               request, 
               cancellationToken);

          return result.Type switch
          {
               Response<CreateWalletResponse>.ResponseType.Conflict
                   => Conflict(),
               Response<CreateWalletResponse>.ResponseType.ValidationError
                   => BadRequest(result.Errors),
               _ => Created(string.Empty, result.Content),
          };
     }

     [HttpGet("{id}")]
     [ProducesResponseType(type: typeof(FindWalletByIdResponse), statusCode: StatusCodes.Status200OK)]
     [ProducesResponseType(statusCode: StatusCodes.Status400BadRequest, type: typeof(List<string>))]
     [ProducesResponseType(statusCode: StatusCodes.Status204NoContent)]
     public async Task<IActionResult> GetAsync(
         Guid id,
         CancellationToken cancellationToken)
     {
          var request = FindWalletByIdRequest.Factory(id);

          var result = await findWalletByIdUseCase.HandleAsync(
               request, 
               cancellationToken);

          return result.Type switch
          {
               Response<FindWalletByIdResponse>.ResponseType.ContentNotExits
                   => NoContent(),
               Response<FindWalletByIdResponse>.ResponseType.ValidationError
                   => BadRequest(result.Errors),
               _ => Ok(result.Content),
          };
     }

     [HttpPatch("{id}/deposits")]
     [ProducesResponseType(type: typeof(CreateWalletResponse), statusCode: StatusCodes.Status201Created)]
     [ProducesResponseType(statusCode: StatusCodes.Status409Conflict, type: typeof(void))]
     [ProducesResponseType(statusCode: StatusCodes.Status400BadRequest, type: typeof(List<string>))]
     public async Task<IActionResult> UpdateBalanceAsync(
         Guid id,
         [FromBody] DepositWalletBalanceModel depositWalletBalanceModel,
         CancellationToken cancellationToken)
     {
          var request = DepositWalletBalanceRequest.Factory(
               walletId: id,
               balance: depositWalletBalanceModel.Balance);

          var result = await depositWalletBalanceUseCase.HandleAsync(
               request, 
               cancellationToken);

          return result.Type switch
          {
               Response<DepositWalletBalanceResponse>.ResponseType.ContentNotExits
                   => NoContent(),
               Response<DepositWalletBalanceResponse>.ResponseType.FailedDependency
                   => StatusCode((int)HttpStatusCode.FailedDependency),
               _ => Ok(result.Content),
          };
     }
}
