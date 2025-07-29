using AutoMapper;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Bibliography;
using IM.Domain.Entities.CarCompanyDomain;
using IM.Dto.V1.CarCompany.Request;
using IM.Dto.V1.CarCompany.Response;
using IM.Repository.V1.CarCompanyRepository.Interface;
using IM.Repository.V1.CarRepository.Interface;
using IM.Service.V1.Admin.CarCompanyService.Helper;
using IM.Service.V1.Admin.CarCompanyService.Interface;
using IM.Service.V1.Admin.CarService.Impl;
using IM.Service.V1.Admin.CarService.Interface;
using Infrastructure.V1.FilterBase.Model;
using Microsoft.AspNetCore.Http;
using Middleware.V1.Request.Model;
using ResponseWrapper.V1;
using ResponseWrapper.V1.Model;

namespace IM.Service.V1.Admin.CarCompanyService.Impl
{
    public class CarCompanySrvc : ICarCompanySrvc
    {
        private readonly IMapper _mapper;
        private readonly ResponseHelper _responseHelper;
        private readonly ICarCompanyRepo _carCompanyRepo;
        private readonly ICarRepo _carRepo;
        private readonly CarCompanyHelper _carCompanyHelper;

        public CarCompanySrvc(IMapper mapper, ICarCompanyRepo carCompanyRepo, ICarRepo carRepo, ResponseHelper responseHelper)
        {
            _mapper = mapper;
            _carCompanyRepo = carCompanyRepo;
            _responseHelper = responseHelper;
            _carCompanyHelper = new CarCompanyHelper();
            _carRepo = carRepo;
        }

        #region For Admin EndPoint

        public async Task<CommonResponse> GetByIdAsync(int cc_id, RequestMetadata data)
        {
            var carCompany = await _carCompanyRepo.GetFirstOrDefault(x => x.cc_id == cc_id && x.cc_lang == data.lang && x.cc_is_active);
            if (carCompany == null)
            {
                return _responseHelper.CreateResponse("RecordNotExist", null, 404, data.lang, false);
            }
            return _responseHelper.CreateResponse("Success", carCompany, 200, data.lang, false);

        }
        public async Task<CommonResponse> LookupAsync(RequestMetadata data)
        {
            // Fetch the record using repository
            var carCompany = await _carCompanyRepo.GetByConditionsAsync(x => x.cc_lang == data.lang && x.cc_is_active == true);
            // If the record is not found, return a proper response
            if (carCompany == null)
            {
                return _responseHelper.CreateResponse("RecordNotFound", null, 404, data.lang, false);
            }

            var response = _mapper.Map<List<CarCompanyLookupModel>>(carCompany);

            // Return success response
            return _responseHelper.CreateResponse("Success", response, 200, data.lang, false);
        }
        public async Task<CommonResponse> AddAsync(CarCompanyAddModel request, RequestMetadata data)
        {
            try
            {
                var carCompany = await _carCompanyRepo.GetFirstOrDefault(x => x.cc_lang == data.lang && x.cc_name == request.cc_name);
                if (carCompany != null)
                {
                    return _responseHelper.CreateResponse("RecordAlreadyExist", null, 400, data.lang, false);
                }
                // Add and Mapp Data
                var responce = await _carCompanyRepo.AddAsync(_carCompanyHelper.MapCarCompany(request, data));

                // Return success response
                return _responseHelper.CreateResponse("AddSuccessful", responce, 200, data.lang, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<CommonResponse> ReadExcelFile(IFormFile file, RequestMetadata data)
        {
            var carCompanies = new List<CarCompanyAddModel>();

            using (var stream = new MemoryStream())
            {
                await file.CopyToAsync(stream);

                using (var workbook = new XLWorkbook(stream))
                {
                    var worksheet = workbook.Worksheet(1); // Get first sheet
                    var rows = worksheet.RangeUsed().RowsUsed(); // Get used rows

                    foreach (var row in rows.Skip(2)) // Skip header row
                    {
                        var carCompany = new CarCompanyAddModel
                        {
                            cc_lang = data.lang,
                            cc_name = row.Cell(2).GetString().Trim(),
                            cc_image = row.Cell(1).GetString().Trim(),
                            cc_desc = row.Cell(3).GetString().Trim()
                        };

                        carCompanies.Add(carCompany);
                    }
                }
            }

            var existingCarCompanies = await _carCompanyRepo.GetAllAsync();
            var existingNames = existingCarCompanies.Select(c => c.cc_name).ToHashSet();

            // Find duplicate records
            var duplicateRecords = carCompanies.Where(car => existingNames.Contains(car.cc_name)).ToList();

            // Find new records that don't exist in the database
            var newCarCompanies = carCompanies
                .Where(car => !existingNames.Contains(car.cc_name))
                .Select(car => _carCompanyHelper.MapCarCompany(car, data))
                .ToList();

            // If there are duplicates, return them with an error message
            if (duplicateRecords.Any())
            {
                return _responseHelper.CreateResponse("DuplicateRecordsFound.", duplicateRecords, 400, data.lang, false);
            }

            // If no new records, return an appropriate message
            if (!newCarCompanies.Any())
            {
                return _responseHelper.CreateResponse("RecordNotFound", null, 400, data.lang, false);
            }

            await _carCompanyRepo.AddRangeAsync(newCarCompanies);
            return _responseHelper.CreateResponse("AddSuccessful", newCarCompanies, 200, data.lang, false);
        }

        public async Task<CommonResponse> UpdateAsync(CarCompanyUpdateModel request, RequestMetadata data)
        {
            // Fetch the record using repository
            var carCompany = await _carCompanyRepo.GetByIdAsync(request.cc_id);

            // If the record is not found, return a proper response
            if (carCompany == null)
            {
                return _responseHelper.CreateResponse("RecordNotFound", null, 404, data.lang, false);
            }
            string companyName = carCompany.cc_name;
            carCompany.cc_lang = data.lang;
            carCompany.cc_name = request.cc_name;
            carCompany.cc_image = request.cc_image;
            carCompany.cc_desc = request.cc_desc;
            carCompany.cc_updated_at = DateTime.UtcNow;
            carCompany.cc_updated_by = data.userId;

            

            if(companyName != request.cc_name)
            {
                var cars = await _carRepo.GetByConditionsAsync(x => x.cr_lang == data.lang && x.cr_fk_cc_id == request.cc_id);
                if(cars.Count > 0)
                {
                    foreach (var car in cars)
                    {
                        car.cr_fk_cc_name = request.cc_name;
                    }

                     await _carRepo.UpdateManyAsync(cars.ToList());
                }
            }
            // Save changes using the repository update method
            await _carCompanyRepo.UpdateAsync(carCompany);

            // Return success response
            return _responseHelper.CreateResponse("UpdateSuccessful", null, 200, data.lang, false);
        }

        public async Task<CommonResponse> PagingAsync(FilterRequest request, RequestMetadata data)
        {
            var response = await _carCompanyRepo.PagingAsync(request, data);

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
            var response = await _carCompanyRepo.ExportAsync(request, data);

            var pagedResponse = new
            {
                response.Items,
                response.TotalCount,
            };

            //Paginated data retrieved successfully.
            return _responseHelper.CreateResponse("ExportedDataSuccess", pagedResponse, 200, data.lang, false);
        }

        public async Task<CommonResponse> DeleteAsync(int cc_id, RequestMetadata data)
        {
            var carCompany = await _carCompanyRepo.GetByIdAsync(cc_id);

            // If the record is not found, return a proper response
            if (carCompany == null || !carCompany.cc_is_active)
            {
                return _responseHelper.CreateResponse("RecordNotFound", null, 404, data.lang, false);
            }
            var car = await _carRepo.GetFirstOrDefault(x => x.cr_fk_cc_id == cc_id && x.cr_lang == data.lang && x.cr_is_active);
            if (car != null)
            {
                return _responseHelper.CreateResponse("CarCompanyDelete", null, 400, data.lang, false);
            }

            carCompany.cc_is_active = false;
            carCompany.cc_updated_at = DateTime.UtcNow;
            carCompany.cc_updated_by = data.userId;

            // Save changes using the repository update method
            await _carCompanyRepo.UpdateAsync(carCompany);

            // Return success response
            return _responseHelper.CreateResponse("DeleteSuccessful", null, 200, data.lang, false);
        }

        #endregion For Admin EndPoint

        #region For Internal Use 
        public async Task<CarCompany> GetFirstOrDefault(int cc_id, RequestMetadata data)
        {
            return await _carCompanyRepo.GetFirstOrDefault(x => x.cc_lang == data.lang && x.cc_id == cc_id && x.cc_is_active);
        }
        public async Task<List<CarCompany>> GetAllAsync(RequestMetadata data)
        {
            var carCompanies = await _carCompanyRepo.GetByConditionsAsync(x => x.cc_lang == data.lang);
            return carCompanies.ToList();
        }
        #endregion
    }
}
