namespace Wallets.UnitTest.Commons;

public abstract class BuilderBase<T> where T : notnull
{
     public abstract T Build();
}