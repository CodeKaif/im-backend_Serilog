using CommonApiCallHelper.V1.Model;

namespace CommonApiCallHelper.V1.Interface
{
    public interface ICommonApiCall
    {
        Task FireForgotPost(CommonPostApiModel commonPostApiModel, string requestEndpoint, string path);
        Task<T> PostDataAsync<T>(CommonPostApiModel commonPostApiModel, string requestEndpoint, string path);
    }
}
