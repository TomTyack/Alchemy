using System.Collections.Generic;

namespace Sitecore.Foundation.AlchemyBase.ResponseWrapper
{
    public interface IWebApiResponse
    {
        bool Success { get; set; }

        IList<IServiceError> Errors { get; set; }
    }

    public interface IWebApiResponse<T> : IWebApiResponse
    {
        T Data { get; set; }
    }
}