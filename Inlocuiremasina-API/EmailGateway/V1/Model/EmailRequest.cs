namespace EmailGateway.V1.Model
{
    public class EmailRequest
    {
        public List<string> to { get; set; } = new List<string>();
        public List<string>? cc { get; set; } = new List<string>();
        public List<string>? bcc { get; set; } = new List<string>();
        public string subject { get; set; }
        public string body { get; set; }
    }
}
