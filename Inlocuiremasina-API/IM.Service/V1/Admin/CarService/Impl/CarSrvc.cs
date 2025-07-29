using AutoMapper;
using ClosedXML.Excel;
using IM.CacheService.V1.Interface;
using IM.Domain.Entities.CarDomain;
using IM.Dto.V1.Car.Request;
using IM.Dto.V1.Car.Response;
using IM.Repository.V1.CarRepository.Interface;
using IM.Service.V1.Admin.CarCompanyService.Interface;
using IM.Service.V1.Admin.CarService.Helper;
using IM.Service.V1.Admin.CarService.Interface;
using Infrastructure.V1.FilterBase.Model;
using Microsoft.AspNetCore.Http;
using Middleware.V1.Request.Model;
using ResponseWrapper.V1;
using ResponseWrapper.V1.Model;

namespace IM.Service.V1.Admin.CarService.Impl
{
    public class CarSrvc : ICarSrvc
    {
        private readonly IMapper _mapper;
        private readonly ResponseHelper _responseHelper;
        private readonly ICarRepo _carRepo;
        private readonly CarHelper _carHelper;
        private readonly ICarCompanySrvc _carCompanyService;
        private readonly ICacheSrvc _cacheSrvc;

        public CarSrvc(IMapper mapper, ICarRepo CarRepo, ICarCompanySrvc carCompanyService, ICacheSrvc cacheSrvc, ResponseHelper responseHelper)
        {
            _mapper = mapper;
            _carRepo = CarRepo;
            _responseHelper = responseHelper;
            _carHelper = new CarHelper();
            _carCompanyService = carCompanyService;
            _cacheSrvc = cacheSrvc;
        }

        #region For Admin EndPoint

        public async Task<CommonResponse> GetByIdAsync(int cr_id, RequestMetadata data)
        {
            var car = await _carRepo.GetFirstOrDefault(x => x.cr_id == cr_id && x.cr_lang == data.lang && x.cr_is_active);
            if (car == null)
            {
                return _responseHelper.CreateResponse("RecordNotExist", null, 404, data.lang, false);
            }
            return _responseHelper.CreateResponse("Success", car, 200, data.lang, false);

        }

        public async Task<CommonResponse> LookupAsync(CarLookupRequest request, RequestMetadata data)
        {

            string cacheKey = $"car_lookup:{data.lang}:{request.cr_model}:{request.cr_fk_cc_name}:{request.cr_name}:{request.cr_is_available}:{request.cr_is_active}";

            var res = await _cacheSrvc.GetAsync<List<CarLookupModel>>(cacheKey);
            if (res != null)
            {
                return _responseHelper.CreateResponse("Success", res, 200, data.lang, false);
            }
            // Fetch the record using repository
            var carList = await _carRepo.GetByConditionsAsync(x => x.cr_lang == data.lang &&
            (request.cr_is_active == null || x.cr_is_active == request.cr_is_active) &&
            (request.cr_is_available == null || x.cr_is_available == request.cr_is_available) &&
            (request.cr_fk_cc_name == "all" || x.cr_fk_cc_name == request.cr_fk_cc_name) &&
            (request.cr_name == "all" || x.cr_name == request.cr_name) &&
            (request.cr_model == "all" || x.cr_model == request.cr_model));
            // If the record is not found, return a proper response
            if (carList == null)
            {
                return _responseHelper.CreateResponse("RecordNotFound", null, 404, data.lang, false);
            }

            var response = _mapper.Map<List<CarLookupModel>>(carList);

            await _cacheSrvc.SetAsync(cacheKey, response);
            // Return success response
            return _responseHelper.CreateResponse("Success", response, 200, data.lang, false);
        }

        public async Task<CommonResponse> ReadExcelFile(IFormFile file, RequestMetadata data)
        {
            var cars = new List<CarImportModel>();

            using (var stream = new MemoryStream())
            {
                await file.CopyToAsync(stream);

                using (var workbook = new XLWorkbook(stream))
                {
                    var worksheet = workbook.Worksheet(1); // Get first sheet
                    var rows = worksheet.RangeUsed().RowsUsed(); // Get used rows

                    foreach (var row in rows.Skip(2)) // Skip header row
                    {
                        var car = new CarImportModel
                        {
                            cr_lang = data.lang,
                            cr_name = row.Cell(3).GetString().Trim(),
                            cr_model = row.Cell(6).GetString().Trim(),
                            cr_image = row.Cell(1).GetString().Trim(),
                            cr_images = row.Cell(2).GetString().Trim(),
                            cr_fk_cc_name = row.Cell(5).GetString().Trim(),
                            cr_is_available = row.Cell(7).GetString().Trim().ToLower() switch
                            {
                                "yes" => true,
                                "no" => false,
                                _ => false
                            },
                            cr_desc = row.Cell(4).GetString().Trim(),
                        };

                        cars.Add(car);
                    }
                }
            }

            var existingLocation = await _carRepo.GetAllAsync();
            var existingNames = existingLocation.Select(c => c.cr_name).ToHashSet();

            var carCompanies = await _carCompanyService.GetAllAsync(data);
            if (carCompanies == null || !carCompanies.Any())
            {
                return _responseHelper.CreateResponse("RecordNotExist", null, 404, data.lang, false);
            }

            var carCompanyNames = carCompanies.Select(cc => cc.cc_name).ToHashSet();
           

            // Find new records that don't exist in the database
            var newData = cars.Where(cr => !existingNames.Contains(cr.cr_name)).Select(cr =>
            {
                var carCompany = carCompanies.FirstOrDefault(cc => cc.cc_name == cr.cr_fk_cc_name);
                return carCompany != null ? _carHelper.MapImportCar(cr, carCompany, data) : null;
            }).Where(mappedCar => mappedCar != null).ToList();


            var missingCarCompanies = newData.Where(car => !carCompanyNames.Contains(car.cr_fk_cc_name)).Select(car => car.cr_fk_cc_name).Distinct().ToList();

            // If any car companies are missing, return an error
            if (missingCarCompanies.Any())
            {
                return _responseHelper.CreateResponse("CarCompanyNotExist", missingCarCompanies, 404, data.lang, false);
            }


            // If no new records, return an appropriate message
            if (!newData.Any())
            {
                return _responseHelper.CreateResponse("RecordNotFound", null, 400, data.lang, false);
            }

            await _carRepo.AddRangeAsync(newData);

            // Remove cache
            await _cacheSrvc.RemoveByPatternAsync($"car_lookup:{data.lang}:*");
            return _responseHelper.CreateResponse("AddSuccessful", newData, 200, data.lang, false);
        }

        public async Task<CommonResponse> AddAsync(CarAddModel request, RequestMetadata data)
        {
            try
            {
                //var car = await _carRepo.GetFirstOrDefault(x => x.cr_lang == data.lang && x.cr_name == request.cr_name && x.cr_is_active);
                //if (car != null)
                //{
                //    return _responseHelper.CreateResponse("RecordAlreadyExist", null, 400, data.lang, false);
                //}
                var carCompany = await _carCompanyService.GetFirstOrDefault(request.cr_fk_cc_id, data);
                if (carCompany == null)
                {
                    return _responseHelper.CreateResponse("RecordNotExist", null, 404, data.lang, false);
                }

                // Add and Mapp Data
                var responce = await _carRepo.AddAsync(_carHelper.MapCar(request, carCompany, data));

                // Remove cache
                await _cacheSrvc.RemoveByPatternAsync($"car_lookup:{data.lang}:*");
                // Return success response
                return _responseHelper.CreateResponse("AddSuccessful", responce, 200, data.lang, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<CommonResponse> UpdateAsync(CarUpdateModel request, RequestMetadata data)
        {
            // Fetch the record using repository
            var car = await _carRepo.GetByIdAsync(request.cr_id);

            // If the record is not found, return a proper response
            if (car == null)
            {
                return _responseHelper.CreateResponse("RecordNotFound", null, 404, data.lang, false);
            }

            var carCompany = await _carCompanyService.GetFirstOrDefault(request.cr_fk_cc_id, data);
            if (carCompany == null)
            {
                return _responseHelper.CreateResponse("RecordNotExist", null, 404, data.lang, false);
            }

            car.cr_lang = data.lang;
            car.cr_name = request.cr_name;
            car.cr_image = request.cr_image;
            car.cr_fk_cc_id = carCompany.cc_id;
            car.cr_fk_cc_name = carCompany.cc_name;
            car.cr_images = request.cr_images;
            car.cr_is_available = request.cr_is_available;
            car.cr_model = request.cr_model;
            car.cr_desc = request.cr_desc;
            car.cr_updated_at = DateTime.UtcNow;
            car.cr_updated_by = data.userId;

            // Save changes using the repository update method
            await _carRepo.UpdateAsync(car);

            // Remove cache
            await _cacheSrvc.RemoveByPatternAsync($"car_lookup:{data.lang}:*");

            // Return success response
            return _responseHelper.CreateResponse("UpdateSuccessful", null, 200, data.lang, false);
        }

        public async Task<CommonResponse> PagingAsync(FilterRequest request, RequestMetadata data)
        {
            var response = await _carRepo.PagingAsync(request, data);

            var pagedResponse = new
            {
                response.Items,
                response.TotalCount,
            };

            //Paginated data retrieved successfully.
            return _responseHelper.CreateResponse("PaginatedDataSuccess", pagedResponse, 200, data.lang, false);
        }

        public async Task<CommonResponse> ExportAsync(FilterRequest request, RequestMetadata data)
        {
            var response = await _carRepo.ExportAsync(request, data);

            var pagedResponse = new
            {
                response.Items,
                response.TotalCount,
            };

            //Paginated data retrieved successfully.
            return _responseHelper.CreateResponse("ExportedDataSuccess", pagedResponse, 200, data.lang, false);
        }

        public async Task<CommonResponse> DeleteAsync(int cr_id, RequestMetadata data)
        {
            var car = await _carRepo.GetByIdAsync(cr_id);

            // If the record is not found, return a proper response
            if (car == null || !car.cr_is_active)
            {
                return _responseHelper.CreateResponse("RecordNotFound", null, 404, data.lang, false);
            }

            car.cr_is_active = false;
            car.cr_updated_at = DateTime.UtcNow;
            car.cr_updated_by = data.userId;

            // Save changes using the repository update method
            await _carRepo.UpdateAsync(car);

            // Remove cache
            await _cacheSrvc.RemoveByPatternAsync($"car_lookup:{data.lang}:*");

            // Return success response
            return _responseHelper.CreateResponse("DeleteSuccessful", null, 200, data.lang, false);
        }
        #endregion

        #region Internal Methods
        public async Task<bool> UpdateCarCompanyNameAsync(int cc_id, string companyName, RequestMetadata data)
        {
            var cars = await _carRepo.GetByConditionsAsync(x => x.cr_lang == data.lang && x.cr_fk_cc_id == cc_id);
            if (cars == null || !cars.Any())
            {
                return false;
            }
            foreach (var car in cars)
            {
                car.cr_fk_cc_name = companyName;
            }

            await _carRepo.UpdateManyAsync(cars.ToList());

            // Remove cache
            await _cacheSrvc.RemoveByPatternAsync($"car_lookup:{data.lang}:*");

            return true;
        }

        public Task<Car> GetByCarCompanyAsync(int cr_fk_cc_id, RequestMetadata data)
        {
            return _carRepo.GetFirstOrDefault(x => x.cr_fk_cc_id == cr_fk_cc_id && x.cr_lang == data.lang && x.cr_is_active);
        }

        public Task<Car> GetCarAsync(int cr_id, RequestMetadata data)
        {
            return _carRepo.GetFirstOrDefault(x => x.cr_id == cr_id && x.cr_lang == data.lang && x.cr_is_active);
        }
        #endregion
    }
}
