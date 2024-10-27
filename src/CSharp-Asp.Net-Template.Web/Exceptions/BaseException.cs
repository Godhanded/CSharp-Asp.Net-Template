namespace CSharp_Asp.Net_Template.Web.Exceptions
{
    public class BaseException : Exception
    {
        public int StatusCode { get; }
        public BaseException(string message, int statusCode = StatusCodes.Status500InternalServerError) 
            : base(message)
        {
            StatusCode = statusCode;
        }
    }
}
