using FluentValidation.Results;

namespace Wallets.UnitTest.Commons.Builders;

public class ValidationResultBuilder : BuilderBase<ValidationResult>
{
  private readonly List<ValidationFailure> _validationFailures = [];

  public ValidationResultBuilder WithFailed()
  {
    _validationFailures.Add(item: new(
      propertyName: FakerSingleton.GetInstance().Faker.Random.Word(),
      errorMessage: FakerSingleton.GetInstance().Faker.Random.Word()));

    return this;
  }
  public override ValidationResult Build() => new(_validationFailures);
}
