using AutoMapper;
using Project.AndroidIosApp.Business.Abstract.Services;
using Project.AndroidIosApp.Business.Concrete.Managers.Constans;
using Project.AndroidIosApp.Core.Enums;
using Project.AndroidIosApp.Core.Utilities.Results.Concrete;
using Project.AndroidIosApp.Core.Utilities.Results.Interface;
using Project.AndroidIosApp.DataAccess.UnitOfWork;
using Project.AndroidIosApp.Dtos.BlogDtos;
using Project.AndroidIosApp.Dtos.SupportUserSupportDtos;
using Project.AndroidIosApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.AndroidIosApp.Business.Concrete.Managers
{
    public class SupportUserSupportManager : ISupportUserSupportService
    {
        private readonly IUow _uow;
        private readonly IMapper _mapper;
        public SupportUserSupportManager(IUow uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<IResponse> DeleteAsync(int id)
        {
            var data = await _uow.GetRepository<SupportUserSupport>().GetByIdAsync(id);
            if(data != null)
            {
                _uow.GetRepository<SupportUserSupport>().Delete(data);
                await _uow.SaveChangesAsync();
                return new Response(ResponseType.Success, SupportUserSupportMessages.DeletedSupportUserSupport);
            }
            else
            {
                return new Response(ResponseType.NotFound, SupportUserSupportMessages.NotDeletedSupportUserSupport);
            }
        }

        public async Task<IDataResponse<List<GetSupportUserSupportDto>>> GetAllAsync()
        {
            var data = _mapper.Map<List<GetSupportUserSupportDto>>(await _uow.GetRepository<SupportUserSupport>().GetAllAsync());
            return new DataResponse<List<GetSupportUserSupportDto>>(ResponseType.Success,data);
        }

        public async Task<IDataResponse<IDto>> GetByIdAsync<IDto>(int id)
        {
            var data = _mapper.Map<IDto>(await _uow.GetRepository<SupportUserSupport>().GetByFilterAsync(x => x.Id == id));
            if(data != null)
            {
                return new DataResponse<IDto>(ResponseType.Success, data);
            }
            else
            {
                return new DataResponse<IDto>(ResponseType.NotFound, $"{id} {SupportUserSupportMessages.NotFoundIdSupportUserSupport}");
            }
        }

        public async Task<IDataResponse<CreateSupportUserSupportDto>> InsertAsync(CreateSupportUserSupportDto createSupportUserSupportDto)
        {
           await _uow.GetRepository<SupportUserSupport>().InsertAsync(_mapper.Map<SupportUserSupport>(createSupportUserSupportDto));
           await _uow.SaveChangesAsync();
           return new DataResponse<CreateSupportUserSupportDto>(ResponseType.Success, createSupportUserSupportDto);
        }

        public async Task<IDataResponse<UpdateSupportUserSupportDto>> UpdateAsync(UpdateSupportUserSupportDto updateSupportUserSupportDto)
        {
           var unChangedData = await _uow.GetRepository<SupportUserSupport>().GetByIdAsync(updateSupportUserSupportDto.Id);
            if(unChangedData != null)
            {
                _uow.GetRepository<SupportUserSupport>().Update(_mapper.Map<SupportUserSupport>(updateSupportUserSupportDto), unChangedData);
                await _uow.SaveChangesAsync();
                return new DataResponse<UpdateSupportUserSupportDto>(ResponseType.Success, updateSupportUserSupportDto);
            }
            else
            {
                return new DataResponse<UpdateSupportUserSupportDto>(ResponseType.NotFound, SupportUserSupportMessages.NotFoundSupportUserSupport);
            }
        }
    }
}
