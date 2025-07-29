using IM.Data.Context;
using IM.Domain.Entities.RequestCarDomain;
using IM.Repository.Core.Generic.Impl;
using IM.Repository.V1.RequestCarRepository.Interface;
using Microsoft.EntityFrameworkCore;

namespace IM.Repository.V1.RequestCarRepository.Impl
{
    public class RequestCarRepo : GenericRepositoryAsync<RequestCar>, IRequestCarRepo
    {
        private readonly DbSet<RequestCar> _requestCar;
        public RequestCarRepo(IMContentDbContext dbContext) : base(dbContext)
        {
            _requestCar = dbContext.Set<RequestCar>();
        }
    }
}
