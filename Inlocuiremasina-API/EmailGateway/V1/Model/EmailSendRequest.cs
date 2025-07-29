using CommonApiCallHelper.V1.Model;

namespace EmailGateway.V1.Model
{
   public class EmailSendRequest : CommonPostApiModel
    {
        public string template { get; set; }
        public string subject { get; set; }
        public bool is_singal_mail { get; set; } = false;
        public List<string> to { get; set; } = new List<string>();
        public List<string>? cc { get; set; } = new List<string>();
        public List<string>? bcc { get; set; } = new List<string>();
        public Dictionary<string, string>? placeholders { get; set; }
        public string lang { get; set; }
    }
}
