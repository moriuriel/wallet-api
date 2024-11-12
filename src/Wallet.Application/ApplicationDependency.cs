using System.Diagnostics.CodeAnalysis;

using FluentValidation;

using Microsoft.Extensions.DependencyInjection;

using Wallets.Application.UseCases.CreateWallet;
using Wallets.Application.UseCases.DepositWalletBalance;
using Wallets.Application.UseCases.FindWalletById;

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
          services.AddScoped<IFindWalletByIdUseCase, FindWalletByIdUseCase>();
          services.AddScoped<IDepositWalletBalanceUseCase, DepositWalletBalanceUseCase>();
          return services;
     }

     public static IServiceCollection AddValidators(this IServiceCollection services)
     {
          services.AddScoped<IValidator<CreateWalletRequest>, CreateWalletRequestValidator>();
          services.AddScoped<IValidator<DepositWalletBalanceRequest>, DepositWalletBalanceRequestValidator>();
          
          return services;
     }
}
