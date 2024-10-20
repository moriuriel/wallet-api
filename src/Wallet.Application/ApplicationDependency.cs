using System.Diagnostics.CodeAnalysis;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Wallets.Application.UseCases.CreateWallet;

namespace Wallets.Application;

[ExcludeFromCodeCoverage]
public static class ApplicationDependency
{
  public static IServiceCollection AddApplicationDependency(this IServiceCollection services)
    => services
      .AddUseCases()
      .AddValidators();
      
  public static IServiceCollection AddUseCases(this IServiceCollection services)
  {
    services.AddScoped<ICreateWalletUseCase, CreateWalletUseCase>();
    return services;
  }

  public static IServiceCollection AddValidators(this IServiceCollection services)
  {
    services.AddScoped<IValidator<CreateWalletRequest>, CreateWalletRequestValidator>();
    return services;
  }
}
