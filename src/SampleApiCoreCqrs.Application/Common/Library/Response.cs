using System;
using System.Collections.Generic;
using SampleApiCoreCqrs.Application.Common.Enums;
using SampleApiCoreCqrs.Application.Common.Interfaces;
using SampleApiCoreCqrs.Application.Common.Model.ResponseModel;

namespace SampleApiCoreCqrs.Application.Common.Library
{
    public class Response : IResponse
    {
        public ResponseType Type { get; set; }
        public ResponseError Error { get; set; }

        public static IResponse Ok()
        {
            return new Response
            {
                Type = ResponseType.OkEmpty
            };
        }

        public static IResponse Bad(string message)
        {
            return new Response
            {
                Type = ResponseType.Bad,
                Error = new ResponseError
                {
                    Message = message,
                    Value = null
                }
            };
        }

        public static IResponse Bad(string message, IEnumerable<ResponseErrorValidation> erros)
        {
            return new Response
            {
                Type = ResponseType.Bad,
                Error = new ResponseError
                {
                    Message = message,
                    Value = erros
                }
            };
        }
    }

    public class Response<TValue> : Response, IResponse<TValue>
    {
        public TValue Value { get; set; }

        public static IResponse<TValue> Ok(TValue value)
        {
            return new Response<TValue>
            {
                Type = ResponseType.OkObject,
                Value = value
            };
        }

        public new static IResponse<TValue> Bad(string message)
        {
            return new Response<TValue>
            {
                Type = ResponseType.Bad,
                Value = default(TValue),
                Error = new ResponseError
                {
                    Message = message,
                    Value = null
                }
            };
        }
    }

    public static class ResponseExtensions
    {
        public static TResponse ConvertTo<TResponse>(this IResponse response)
            where TResponse : IResponse
        {
            TResponse result;

            var type = typeof(TResponse);
            var genericResultType = typeof(Response<>);

            if (type.IsGenericType && type.Name.Equals(genericResultType.Name))
            {
                var genericArgs = type.GetGenericArguments();
                var finalType = genericResultType.MakeGenericType(genericArgs);
                result = (TResponse)Activator.CreateInstance(finalType);
            }
            else
            {
                var targetType = typeof(TResponse);
                if (targetType.IsGenericType)
                {
                    var genericImplementationType = typeof(Response<>);
                    var genericArgs = targetType.GetGenericArguments();
                    var finalType = genericImplementationType.MakeGenericType(genericArgs);
                    result = (TResponse)Activator.CreateInstance(finalType);
                }
                else
                {
                    var nonGenericResult = new Response();
                    result = (TResponse)(IResponse)nonGenericResult;
                }
            }

            result.Error = response.Error;
            result.Type = response.Type;
            return result;
        }

        public static IResponse<TValue> ConvertTo<TResult, TValue>(this IResponse response)
            where TResult : IResponse<TValue>
        {
            var res = new Response<TValue>();

            res.Error = response.Error;
            res.Type = response.Type;
            return res;
        }

        public static TResponse GetResult<TResponse>(this IResponse response)
        {
            TResponse result = default(TResponse);

            if (response is Response<TResponse>)
                result = ((Response<TResponse>)response).Value;

            return result;
        }
    }
}
