using AutoMapper;
using FluentValidation;
using FluentValidation.Validators;
using Microsoft.EntityFrameworkCore;
using Project.AndroidIosApp.Business.Abstract.Services;
using Project.AndroidIosApp.Business.Concrete.Managers.Constans;
using Project.AndroidIosApp.Business.Extensions;
using Project.AndroidIosApp.Core.Enums;
using Project.AndroidIosApp.Core.Utilities.Results.Concrete;
using Project.AndroidIosApp.Core.Utilities.Results.Interface;
using Project.AndroidIosApp.DataAccess.Abstract.Repositories;
using Project.AndroidIosApp.DataAccess.Concrete.Repositories;
using Project.AndroidIosApp.DataAccess.UnitOfWork;
using Project.AndroidIosApp.Dtos.BlogDtos;
using Project.AndroidIosApp.Dtos.ContactDtos;
using Project.AndroidIosApp.Dtos.DeviceDtos;
using Project.AndroidIosApp.Dtos.SupportDtos;
using Project.AndroidIosApp.Entities;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.AndroidIosApp.Business.Concrete.Managers
{
    public class DeviceManager : IDeviceService
    {
        private readonly IUow _uow;
        private readonly IValidator<CreateDeviceDto> _createDeviceDtoValidator;
        private readonly IValidator<UpdateDeviceDto> _updateDeviceDtoValidator;
        private readonly IMapper _mapper;
        private readonly IDeviceDal _deviceDal;
        public DeviceManager(IUow uow, IValidator<CreateDeviceDto> createDeviceDtoValidator, IValidator<UpdateDeviceDto> updateDeviceDtoValidator, IMapper mapper, IDeviceDal deviceDal)
        {
            _uow = uow;
            _createDeviceDtoValidator = createDeviceDtoValidator;
            _updateDeviceDtoValidator = updateDeviceDtoValidator;
            _mapper = mapper;
            _deviceDal = deviceDal;
        }

        public async Task<IDataResponse<List<GetDeviceDto>>> GetAllAsync()
        {
            var entity = await _uow.GetRepository<Device>().GetAllAsync();
            var dto = new List<GetDeviceDto>();
            foreach (var item in entity)
            {
                dto.Add(new GetDeviceDto()
                {
                    Id = item.Id,
                    DeviceName = item.DeviceName,
                    CPU = item.CPU,
                    GPU = item.GPU,
                    MEM = item.MEM,
                    UX = item.UX,
                    TotalScore = item.TotalScore,
                    Price = item.Price,
                    ReleaseYear = item.ReleaseYear,
                    CreateDate = item.CreateDate,
                    ImageUrl = item.ImageUrl,
                    Status = item.Status,
                });
            }
            return new DataResponse<List<GetDeviceDto>>(Core.Enums.ResponseType.Success,dto);
        }
        public async Task<IDataResponse<List<GetDeviceDto>>> GetAllWithOSAndDeviceTypeAsync()
        {
            //UOW Dependenciyi sayesinde sadece genericRepositorydeki metotları çağırabiliyorum.
            var data = _mapper.Map<List<GetDeviceDto>>(await _deviceDal.GetAllWithOSAndDeviceTypeAsync());
            return new DataResponse<List<GetDeviceDto>>(ResponseType.Success, data);
        }
        public async Task<IDataResponse<List<GetDeviceDto>>> GetAllBySortingToCreateDateWithOsDeviceTypeAsync()
        {
            //var entity = await _uow.GetRepository<Device>().GetAllBySorting(x => x.CreateDate, OrderByType.DESC);
            //var dto = new List<GetDeviceDto>();
            //foreach (var item in entity)
            //{
            //    dto.Add(new GetDeviceDto()
            //    {
            //        Id = item.Id,
            //        DeviceName = item.DeviceName,
            //        CPU = item.CPU,
            //        GPU = item.GPU,
            //        MEM = item.MEM,
            //        UX = item.UX,
            //        TotalScore = item.TotalScore,
            //        Price = item.Price,
            //        ReleaseYear = item.ReleaseYear,
            //        CreateDate = item.CreateDate,
            //        ImageUrl = item.ImageUrl,
            //        Status = item.Status,
            //    });
            //}
            //return new DataResponse<List<GetDeviceDto>>(ResponseType.Success, dto);
            //YUKARIDAKİ YAPI İŞİME YARAMADI ÇÜNKÜ OS TABLOSUNU INCLUDE EDEMİYORDUM.
            //BU AŞAĞIDAKİ YAPIYI İSTERSEM REPOSİTORYDE KURUP BURDA O LİSTELEME METOTUNU ÇAĞIRIP KULLANABİLİRİM GetQuery(); KULLANMADAN NORMAL BİR LİSTELEME MOTOTU GİBİ YUKARIDA ÖRNEĞİNİ YAPTIM. 
            var query = _uow.GetRepository<Device>().GetQuery();
            //artık elimde IQeuyable bir şey var.
            var list = await query.OrderByDescending(x => x.CreateDate).Include(x => x.OS).Include(x=> x.DeviceType).ToListAsync();
            var mappingData = _mapper.Map<List<GetDeviceDto>>(list);
         
            return new DataResponse<List<GetDeviceDto>>(ResponseType.Success, mappingData);
        }
        public async Task<IDataResponse<List<GetDeviceDto>>> GetAllBySortingToTotalScoreWithOsDeviceTypeAsync()
        {
            var query = _uow.GetRepository<Device>().GetQuery();
            var list = await query.OrderByDescending(x => x.TotalScore).Include(x => x.OS).Include(x => x.DeviceType).ToListAsync();
            var mappingData = _mapper.Map<List<GetDeviceDto>>(list);
            return new DataResponse<List<GetDeviceDto>>(ResponseType.Success, mappingData);
        }
        public async Task<IDataResponse<IDto>> GetByIdAsync<IDto>(int id)
        {
            //var entity = await _uow.GetRepository<Device>().GetByFilterAsync(x => x.Id == id);
            //var dto = new GetDeviceDto()
            //{
            //    Id = entity.Id,
            //    DeviceName = entity.DeviceName,
            //    CPU = entity.CPU,
            //    GPU = entity.GPU,
            //    MEM = entity.MEM,
            //    UX = entity.UX,
            //    TotalScore = entity.TotalScore,
            //    Price = entity.Price,
            //    ReleaseYear = entity.ReleaseYear,
            //    CreateDate = item.CreateDate,
            //    ImageUrl = item.ImageUrl,
            //    Status = item.Status,
            //};
            //return dto;
            var entity = _mapper.Map<IDto>(await _uow.GetRepository<Device>().GetByFilterAsync(x => x.Id == id));
            if(entity != null)
            {
                return new DataResponse<IDto>(ResponseType.Success, entity);
            }
            else
            {
                return new DataResponse<IDto>(ResponseType.NotFound, $"{id} {DeviceMessages.NotFoundIdDevice}");
            }
        }
        public async Task<IDataResponse<CreateDeviceDto>> InsertAsync(CreateDeviceDto createDeviceDto)
        {
            var validationResult = _createDeviceDtoValidator.Validate(createDeviceDto);
            if (validationResult.IsValid)
            {
                var entity = new Device()
                {
                    DeviceName = createDeviceDto.DeviceName,
                    CPU = createDeviceDto.CPU,
                    GPU = createDeviceDto.GPU,
                    MEM = createDeviceDto.MEM,
                    UX = createDeviceDto.UX,
                    TotalScore = createDeviceDto.TotalScore,
                    Price = createDeviceDto.Price,
                    ReleaseYear = createDeviceDto.ReleaseYear,
                    ImageUrl = createDeviceDto.ImageUrl,
                    Status = createDeviceDto.Status
                };
                await _uow.GetRepository<Device>().InsertAsync(entity);
                await _uow.SaveChangesAsync();
                return new DataResponse<CreateDeviceDto>(ResponseType.Success, createDeviceDto);
            }
            else
            {
                return new DataResponse<CreateDeviceDto>(ResponseType.ValidationError, createDeviceDto, validationResult.ConverToCustomValidationError());
            }
        }

        public async Task<IResponse> DeleteAsync(int id)
        {
            var data = await _uow.GetRepository<Device>().GetByIdAsync(id);
            if(data != null)
            {
                _uow.GetRepository<Device>().Delete(data);
                await _uow.SaveChangesAsync();
                return new Response(ResponseType.Success, DeviceMessages.DeletedDevice);
            }
            return new Response(ResponseType.NotFound, DeviceMessages.NotDeletedDevice);
        }

        public async Task<IDataResponse<UpdateDeviceDto>> UpdateAsync(UpdateDeviceDto updateDeviceDto)
        {
            var validationResult = _updateDeviceDtoValidator.Validate(updateDeviceDto);
            if (validationResult.IsValid)
            {
                var unChangedData = await _uow.GetRepository<Device>().GetByIdAsync(updateDeviceDto.Id);
                if(unChangedData != null)
                {
                    _uow.GetRepository<Device>().Update(new Device()
                    {
                        DeviceName = updateDeviceDto.DeviceName,
                        CPU = updateDeviceDto.CPU,
                        GPU = updateDeviceDto.GPU,
                        MEM = updateDeviceDto.MEM,
                        UX = updateDeviceDto.UX,
                        TotalScore = updateDeviceDto.TotalScore,
                        Price = updateDeviceDto.Price,
                        ReleaseYear = updateDeviceDto.ReleaseYear,
                        ImageUrl = updateDeviceDto.ImageUrl,
                        Status = updateDeviceDto.Status,
                    }, unChangedData);
                    await _uow.SaveChangesAsync();
                    return new DataResponse<UpdateDeviceDto>(ResponseType.Success, updateDeviceDto);
                }
                else
                {
                    return new DataResponse<UpdateDeviceDto>(ResponseType.NotFound, DeviceMessages.NotFoundDevice);
                }
            }
            else
            {
                return new DataResponse<UpdateDeviceDto>(ResponseType.ValidationError, updateDeviceDto, validationResult.ConverToCustomValidationError());
            }
        }

        public async Task<IDataResponse<GetDeviceDto>> GetByIdWithOSDeviceTypeCommentAsync(int id)
        {
            var query = _uow.GetRepository<Device>().GetQuery();
            var data = await query.Where(x => x.Id == id).Include(x=> x.OS).Include(x=> x.DeviceType).Include(x=> x.Comments).ThenInclude(x=> x.ProjectUser).FirstOrDefaultAsync();
            var mappingData = _mapper.Map<GetDeviceDto>(data);
            if(mappingData != null)
            {
                return new DataResponse<GetDeviceDto>(ResponseType.Success, mappingData);
            }
            return new DataResponse<GetDeviceDto>(ResponseType.NotFound, $"{id} {DeviceMessages.NotFoundIdDevice}");
        }
    }
}
