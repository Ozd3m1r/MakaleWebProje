namespace MakaleWebProje.Extensions
{
    public static class HttpRequestExtension
    {
        public static string PathAndQuerry(this HttpRequest request)
        {
            return request.QueryString.HasValue
                ? $"{request.Path}{request.QueryString}"
                : request.Path.ToString();
        }
    }
}
