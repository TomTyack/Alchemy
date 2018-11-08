
namespace Sitecore.Foundation.AlchemyBase.ResponseWrapper
{
    public interface IServiceError
    {
        string ErrorCode { get; set; }

        string Message { get; set; }
    }

    public class ServiceError : IServiceError
    {
        public ServiceError()
        {
        }

        public ServiceError(string message, string errorCode)
        {
            Message = message;
            ErrorCode = errorCode;
        }

        public string ErrorCode { get; set; }
        public string Message { get; set; }
    }
}