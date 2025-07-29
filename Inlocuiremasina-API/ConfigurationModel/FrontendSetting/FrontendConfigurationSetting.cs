using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigurationModel.FrontendSetting
{
    public class FrontendConfigurationSetting
    {
        public string FrontendUrl { get; set; }

        public AccountFrontUrl AccountUrls { get; set; }
    }

    public class AccountFrontUrl
    {
        public string ResetPasswordUrl { get; set; }
    }
}
