using SampleApiCoreCqrs.Application.Common.Enums;
using SampleApiCoreCqrs.Application.Common.Model.ResponseModel;

namespace SampleApiCoreCqrs.Application.Common.Interfaces
{
    public interface IResponse
    {
        ResponseType Type { get; set; }
        ResponseError Error { get; set; }
    }

    public interface IResponse<TValue> : IResponse
    {
        TValue Value { get; set; }
    }
}
