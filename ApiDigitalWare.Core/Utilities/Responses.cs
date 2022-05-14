namespace ApiDigitalWare.Core.Utilities
{
    public class ResponsesUtilities
    {
        public static object ParseResponse(int StatusCode, string message, object data)
        {
            var response = new
            {
                ContentType = "application/json",
                StatusCode,
                message,
                data,
            };

            var jsonResponse = new { response };
            return jsonResponse;
        }
    }
}
