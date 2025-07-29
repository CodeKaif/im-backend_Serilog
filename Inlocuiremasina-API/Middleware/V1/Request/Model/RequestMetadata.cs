using System;

namespace Middleware.V1.Request.Model
{
    public class RequestMetadata
    {
        public string lang { get; set; } = "en";  // Default value
        public int userId { get; set; } = 0; // Default value
        public string correlationId { get; set; } = Guid.NewGuid().ToString();
    }
}
