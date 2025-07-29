using IM.Notification.Dto;

namespace IM.Notification.ServiceExtension.MapperExtension
{
    public static class MapperExtension
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(IMappingProfileMarker).Assembly);
        }
    }
}
