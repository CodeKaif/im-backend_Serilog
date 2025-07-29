using IM.Service.V1.Admin.TermConditionsService.Impl;
using IM.Service.V1.Admin.TermConditionsService.Interface;
using Microsoft.Extensions.DependencyInjection;

namespace IM.Service.V1.Admin.TermConditionsService.Extension
{
    public class TermConditionsSrvcExtension
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddTransient<ITermConditionsSrvc, TermConditionsSrvc>();
        }
    }
}
