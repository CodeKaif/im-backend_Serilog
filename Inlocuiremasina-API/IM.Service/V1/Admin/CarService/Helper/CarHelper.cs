using IM.Domain.Entities.CarCompanyDomain;
using IM.Domain.Entities.CarDomain;
using IM.Dto.V1.Car.Request;
using Middleware.V1.Request.Model;

namespace IM.Service.V1.Admin.CarService.Helper
{
    public class CarHelper
    {
        public Car MapCar(CarAddModel request, CarCompany carCompany,  RequestMetadata data)
        {
            return new Car
            {
                cr_lang = data.lang,
                cr_name = request.cr_name,
                cr_image = request.cr_image,
                cr_fk_cc_id = carCompany.cc_id,
                cr_fk_cc_name = carCompany.cc_name,
                cr_images = request.cr_images,
                cr_is_available = request.cr_is_available,
                cr_model = request.cr_model,
                cr_desc = request.cr_desc,
                cr_is_active = true,
                cr_created_by = data.userId,
                cr_created_at = DateTime.UtcNow,
                cr_updated_by = data.userId,
                cr_updated_at = DateTime.UtcNow,
            };
        }

        public Car MapImportCar(CarImportModel request, CarCompany carCompany, RequestMetadata data)
        {
            return new Car
            {
                cr_lang = data.lang,
                cr_name = request.cr_name,
                cr_image = request.cr_image,
                cr_fk_cc_id = carCompany.cc_id,
                cr_fk_cc_name = carCompany.cc_name,
                cr_images = request.cr_images,
                cr_is_available = request.cr_is_available,
                cr_model = request.cr_model,
                cr_desc = request.cr_desc,
                cr_is_active = true,
                cr_created_by = data.userId,
                cr_created_at = DateTime.UtcNow,
                cr_updated_by = data.userId,
                cr_updated_at = DateTime.UtcNow,
            };
        }
    }
}
