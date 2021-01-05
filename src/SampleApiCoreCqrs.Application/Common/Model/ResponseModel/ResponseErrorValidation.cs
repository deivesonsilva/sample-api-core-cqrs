namespace SampleApiCoreCqrs.Application.Common.Model.ResponseModel
{
    public class ResponseErrorValidation
    {
        public string Field { get; set; }
        public string Message { get; set; }

        public ResponseErrorValidation(string field, string message)
        {
            Field = field;
            Message = message;
        }
    }
}
