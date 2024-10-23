using Asp.Versioning;

using Microsoft.AspNetCore.Mvc;

using Wallets.Application.Commons;
using Wallets.Application.UseCases.CreateWallet;

namespace Wallets.Api.Controllers.v1;

[ApiController]
[ApiVersion("1")]
[Route("v{version:apiVersion}/wallets")]
public class WalletsController(ICreateWalletUseCase createWalletUseCase) : ControllerBase
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
}
