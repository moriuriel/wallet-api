﻿namespace Wallets.Application.Commons;
public class Response<T>
{
    private Response(
        ResponseType type,
        T? content)
    {
        Content = content;
        Type = type;
    }

    private Response(ResponseType type)
        => Type = type;

    public T? Content { get; private set; }
    public ResponseType Type { get; private set; }

    public static Response<T> Success(T? content)
        => new(type: ResponseType.Success, content);

    public static Response<T> ContentNotExists()
        => new(type: ResponseType.ContentNotExits);

    public static Response<T> Created(T? content)
        => new(type: ResponseType.Created, content);

    public enum ResponseType
    {
        Success,
        Conflict,
        BusinessRuleFiled,
        ContentNotExits,
        Created
    }
}
