using System.ComponentModel.DataAnnotations;

namespace IM.Notification.Domain.Entities.ApplicationLogsDomain
{
    public class ApplicationLogs
    {
        [Key]
        public int Id { get; set; }
        public string? ServiceName { get; set; }
        public int? UserId { get; set; }
        public string? Language { get; set; }
        public string? Method { get; set; }
        public string? Route { get; set; }
        public int? StatusCode { get; set; }
        public string? AuthorizationToken { get; set; }
        public int? ResponseTimeMs { get; set; }
        public string? ErrorMessage { get; set; }
        public string? StackTrace { get; set; }
        public string? FileName { get; set; }
        public int? LineNumber { get; set; }
    }
}
