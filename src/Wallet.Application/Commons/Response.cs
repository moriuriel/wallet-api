using FluentValidation.Results;

namespace Wallets.Application.Commons;
public class Response<T>
{
     private Response(
         ResponseType type,
         T? content = default,
         List<string>? errors = default)
     {
          Content = content;
          Type = type;
          Errors = errors;
     }

     public T? Content { get; private set; }
     public ResponseType Type { get; private set; }
     public List<string>? Errors { get; private set; }

     public static Response<T> Success(T? content)
         => new(type: ResponseType.Success, content);

     public static Response<T> ContentNotExists()
         => new(type: ResponseType.ContentNotExits);

     public static Response<T> Conflict()
         => new(type: ResponseType.Conflict);

     public static Response<T> Created(T? content)
         => new(type: ResponseType.Created, content);

     public static Response<T> BusinessRuleField()
         => new(type: ResponseType.BusinessRuleFiled);
     public static Response<T> ValidationError(
         ValidationResult validationResult)
         => new(
             type: ResponseType.ValidationError,
             errors: validationResult.Errors.Select(
                 _ => _.ErrorMessage).ToList());
     public enum ResponseType
     {
          Success,
          Conflict,
          BusinessRuleFiled,
          ContentNotExits,
          Created,
          ValidationError,
     }
}
