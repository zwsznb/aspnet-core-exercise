namespace zws.Core.Common.Mvc
{
    public class RequestMethodAttribute : Attribute
    {
        public RequestMethods _method;
        public RequestMethodAttribute(RequestMethods method)
        {
            _method = method;
        }
        public RequestMethodAttribute()
        {
            _method = RequestMethods.GET;
        }
    }
}
