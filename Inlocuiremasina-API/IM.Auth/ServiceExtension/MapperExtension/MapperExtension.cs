using Auth.Dto.V1;

namespace IM.Auth.ServiceExtension.MapperExtension
{
    public static class MapperExtension
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(IMappingProfileMarker).Assembly);
        }
    }
}
