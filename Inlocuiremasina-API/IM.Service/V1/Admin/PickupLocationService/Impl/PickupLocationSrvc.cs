using AutoMapper;
using ClosedXML.Excel;
using IM.Dto.V1.PickupLocation.Request;
using IM.Dto.V1.PickupLocation.Response;
using IM.Repository.V1.PickupLocationRepository.Interface;
using IM.Service.V1.Admin.PickupLocationService.Helper;
using IM.Service.V1.Admin.PickupLocationService.Interface;
using Infrastructure.V1.FilterBase.Model;
using Microsoft.AspNetCore.Http;
using Middleware.V1.Request.Model;
using ResponseWrapper.V1;
using ResponseWrapper.V1.Model;

namespace IM.Service.V1.Admin.PickupLocationService.Impl
{
    public class PickupLocationSrvc : IPickupLocationSrvc
    {
        private readonly IMapper _mapper;
        private readonly ResponseHelper _responseHelper;
        private readonly IPickupLocationRepo _pickupLocationRepo;
        private readonly PickupLocationHelper _pickupLocationHelper;
        public PickupLocationSrvc(IMapper mapper, IPickupLocationRepo pickupLocationRepo, ResponseHelper responseHelper)
        {
            _mapper = mapper;
            _pickupLocationRepo = pickupLocationRepo;
            _responseHelper = responseHelper;
            _pickupLocationHelper = new PickupLocationHelper();
        }

        #region For Admin EndPoint

        public async Task<CommonResponse> GetByIdAsync(int pl_id, RequestMetadata data)
        {
            var pickupLocation = await _pickupLocationRepo.GetFirstOrDefault(x => x.pl_id == pl_id && x.pl_lang == data.lang && x.pl_is_active);
            if (pickupLocation == null)
            {
                return _responseHelper.CreateResponse("RecordNotExist", null, 404, data.lang, false);
            }
            return _responseHelper.CreateResponse("Success", pickupLocation, 200, data.lang, false);
        }

        public async Task<CommonResponse> LookupAsync(RequestMetadata data)
        {
            // Fetch the record using repository
            var pickupLocation = await _pickupLocationRepo.GetByConditionsAsync(x => x.pl_lang == data.lang && x.pl_is_active == true);
            // If the record is not found, return a proper response
            if (pickupLocation == null)
            {
                return _responseHelper.CreateResponse("RecordNotFound", null, 404, data.lang, false);
            }

            var response = _mapper.Map<List<PickupLocationLookupModel>>(pickupLocation);

            // Return success response
            return _responseHelper.CreateResponse("Success", response, 200, data.lang, false);
        }
        public async Task<CommonResponse> AddAsync(PickupLocationAddModel request, RequestMetadata data)
        {
            try
            {
                var pickupLocation = await _pickupLocationRepo.GetFirstOrDefault(x => x.pl_lang == data.lang && x.pl_name == request.pl_name && x.pl_is_active);
                if (pickupLocation != null)
                {
                    return _responseHelper.CreateResponse("RecordAlreadyExist", null, 400, data.lang, false);
                }

                // Add and Mapp Data
                var responce = await _pickupLocationRepo.AddAsync(_pickupLocationHelper.MapPickupLocation(request, data));

                // Return success response
                return _responseHelper.CreateResponse("AddSuccessful", responce, 200, data.lang, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<CommonResponse> UpdateAsync(PickupLocationUpdateModel request, RequestMetadata data)
        {
            // Fetch the record using repository
            var pickupLocation = await _pickupLocationRepo.GetByIdAsync(request.pl_id);

            // If the record is not found, return a proper response
            if (pickupLocation == null || !pickupLocation.pl_is_active)
            {
                return _responseHelper.CreateResponse("RecordNotFound", null, 404, data.lang, false);
            }

            pickupLocation.pl_address = request.pl_address ?? pickupLocation.pl_address;
            pickupLocation.pl_landmark = request.pl_landmark ?? pickupLocation.pl_landmark;
            pickupLocation.pl_lat = request.pl_lat ?? pickupLocation.pl_lat;
            pickupLocation.pl_long = request.pl_long ?? pickupLocation.pl_long;
            pickupLocation.pl_map_iframe = request.pl_map_iframe ?? pickupLocation.pl_map_iframe;
            pickupLocation.pl_name = request.pl_name ?? pickupLocation.pl_name;
            pickupLocation.pl_pincode = request.pl_pincode ?? pickupLocation.pl_pincode;
            pickupLocation.pl_updated_at = DateTime.UtcNow;
            pickupLocation.pl_updated_by = data.userId;

            // Save changes using the repository update method
            await _pickupLocationRepo.UpdateAsync(pickupLocation);

            // Return success response
            return _responseHelper.CreateResponse("UpdateSuccessful", null, 200, data.lang, false);
        }

        public async Task<CommonResponse> PagingAsync(FilterRequest request, RequestMetadata data)
        {
            var response = await _pickupLocationRepo.PagingAsync(request, data);

            var pagedResponse = new
            {
                response.Items,
                response.TotalCount,
            };

            //Paginated data retrieved successfully.
            return _responseHelper.CreateResponse("PaginatedDataSuccess", pagedResponse, 200, data.lang, false);
        }

        public async Task<CommonResponse> ReadExcelFile(IFormFile file, RequestMetadata data)
        {
            var pickupLocation = new List<PickupLocationAddModel>();

            using (var stream = new MemoryStream())
            {
                await file.CopyToAsync(stream);

                using (var workbook = new XLWorkbook(stream))
                {
                    var worksheet = workbook.Worksheet(1); // Get first sheet
                    var rows = worksheet.RangeUsed().RowsUsed(); // Get used rows

                    foreach (var row in rows.Skip(2)) // Skip header row
                    {
                        var location = new PickupLocationAddModel
                        {
                            pl_name = row.Cell(1).GetString().Trim(),
                            pl_lat = row.Cell(3).GetString().Trim(),
                            pl_long = row.Cell(4).GetString().Trim(),
                            pl_address = row.Cell(2).GetString().Trim(),
                            pl_landmark = row.Cell(5).GetString().Trim(),
                            pl_pincode = row.Cell(6).GetString().Trim(),
                            pl_map_iframe = row.Cell(7).GetString().Trim(),
                           
                        };

                        pickupLocation.Add(location);
                    }
                }
            }

            var existingLocation = await _pickupLocationRepo.GetAllAsync();
            var existingNames = existingLocation.Select(c => c.pl_name).ToHashSet();

            // Find duplicate records
            var duplicateRecords = pickupLocation.Where(car => existingNames.Contains(car.pl_name)).ToList();

            // Find new records that don't exist in the database
            var newData = pickupLocation
                .Where(pl => !existingNames.Contains(pl.pl_name))
                .Select(pl => _pickupLocationHelper.MapPickupLocation(pl, data))
                .ToList();

            // If there are duplicates, return them with an error message
            if (duplicateRecords.Any())
            {
                return _responseHelper.CreateResponse("DuplicateRecordsFound.", duplicateRecords, 400, data.lang, false);
            }

            // If no new records, return an appropriate message
            if (!newData.Any())
            {
                return _responseHelper.CreateResponse("RecordNotFound", null, 400, data.lang, false);
            }

            await _pickupLocationRepo.AddRangeAsync(newData);
            return _responseHelper.CreateResponse("AddSuccessful", newData, 200, data.lang, false);
        }

        public async Task<CommonResponse> ExportAsync(FilterRequest request, RequestMetadata data)
        {
            var response = await _pickupLocationRepo.ExportAsync(request, data);

            var pagedResponse = new
            {
                response.Items,
                response.TotalCount,
            };

            //Paginated data retrieved successfully.
            return _responseHelper.CreateResponse("ExportedDataSuccess", pagedResponse, 200, data.lang, false);
        }

        public async Task<CommonResponse> DeleteAsync(int pl_id, RequestMetadata data)
        {
            var pickupLocation = await _pickupLocationRepo.GetByIdAsync(pl_id);

            // If the record is not found, return a proper response
            if (pickupLocation == null || !pickupLocation.pl_is_active)
            {
                return _responseHelper.CreateResponse("RecordNotFound", null, 404, data.lang, false);
            }

            pickupLocation.pl_is_active = false;
            pickupLocation.pl_updated_at = DateTime.UtcNow;
            pickupLocation.pl_updated_by = data.userId;

            // Save changes using the repository update method
            await _pickupLocationRepo.UpdateAsync(pickupLocation);

            // Return success response
            return _responseHelper.CreateResponse("DeleteSuccessful", null, 200, data.lang, false);
        }

        #endregion For Admin EndPoint

    }
}
