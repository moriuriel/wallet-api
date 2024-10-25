using Asp.Versioning;

using Microsoft.AspNetCore.Mvc;

using Wallets.Application.Commons;
using Wallets.Application.UseCases.CreateWallet;
using Wallets.Application.UseCases.FindWalletById;

namespace Wallets.Api.Controllers.v1;

[ApiController]
[ApiVersion("1")]
[Route("v{version:apiVersion}/wallets")]
public class WalletsController(
     ICreateWalletUseCase createWalletUseCase,
     IFindWalletByIdUseCase findWalletByIdUseCase) : ControllerBase
{
     [HttpPost()]
     [ProducesResponseType(type: typeof(CreateWalletResponse), statusCode: StatusCodes.Status201Created)]
     [ProducesResponseType(statusCode: StatusCodes.Status409Conflict, type: typeof(void))]
     [ProducesResponseType(statusCode: StatusCodes.Status400BadRequest, type: typeof(List<string>))]
     public async Task<IActionResult> PostAsync(
         [FromBody] CreateWalletRequest request,
         CancellationToken cancellationToken)
     {
          var result = await createWalletUseCase.HandleAsync(request, cancellationToken);

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
     [ProducesResponseType(type: typeof(CreateWalletResponse), statusCode: StatusCodes.Status201Created)]
     [ProducesResponseType(statusCode: StatusCodes.Status409Conflict, type: typeof(void))]
     [ProducesResponseType(statusCode: StatusCodes.Status400BadRequest, type: typeof(List<string>))]
     public async Task<IActionResult> GetAsync(
         Guid id,
         CancellationToken cancellationToken)
     {
          var request = FindWalletByIdRequest.Factory(id);

          var result = await findWalletByIdUseCase.HandleAsync(request, cancellationToken);

          return result.Type switch
          {
               Response<FindWalletByIdResponse>.ResponseType.Conflict
                   => NoContent(),
               Response<FindWalletByIdResponse>.ResponseType.ValidationError
                   => BadRequest(result.Errors),
               _ => Ok(result.Content),
          };
     }
}
