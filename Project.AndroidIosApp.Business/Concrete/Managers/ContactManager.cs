using AutoMapper;
using FluentValidation;
using Project.AndroidIosApp.Business.Abstract.Services;
using Project.AndroidIosApp.Business.Concrete.Managers.Constans;
using Project.AndroidIosApp.Business.Extensions;
using Project.AndroidIosApp.Core.Enums;
using Project.AndroidIosApp.Core.Utilities.Results.Concrete;
using Project.AndroidIosApp.Core.Utilities.Results.Interface;
using Project.AndroidIosApp.DataAccess.UnitOfWork;
using Project.AndroidIosApp.Dtos.BlogDtos;
using Project.AndroidIosApp.Dtos.ContactDtos;
using Project.AndroidIosApp.Dtos.Interfaces;
using Project.AndroidIosApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

namespace Project.AndroidIosApp.Business.Concrete.Managers
{
    public class ContactManager : IContactService
    {
        private readonly IUow _uow;
        private readonly IValidator<CreateContactDto> _createContactDtoValidator;
        private readonly IValidator<UpdateContactDto> _updateContactDtoValidator;
        private readonly IMapper _mapper;

        public ContactManager(IUow uow, IValidator<CreateContactDto> createContactDtoValidator, IValidator<UpdateContactDto> updateContactDtoValidator, IMapper mapper)
        {
            _uow = uow;
            _createContactDtoValidator = createContactDtoValidator;
            _updateContactDtoValidator = updateContactDtoValidator;
            _mapper = mapper;
        }

        public async Task<IResponse> DeleteAsync(int id)
        {
            var value = await _uow.GetRepository<Contact>().GetByIdAsync(id);
            if (value != null)
            {
                _uow.GetRepository<Contact>().Delete(value);
                await _uow.SaveChangesAsync();
                return new Response(ResponseType.Success, ContactMessages.DeletedContact);
            }
            else
            {
                return new Response(ResponseType.NotFound, ContactMessages.NotDeletedContact);
            }
        }

        public async Task<IDataResponse<List<GetContactDto>>> GetAllAsync()
        {
            var data = await _uow.GetRepository<Contact>().GetAllAsync();
            var dto = new List<GetContactDto>();
            foreach (var item in data)
            {
                dto.Add(new GetContactDto()
                {
                    Id = item.Id,
                    Title = item.Title,
                    Content = item.Content,
                    Adress = item.Adress,
                    Skype = item.Skype,
                    Mail = item.Mail,
                    Phone = item.Phone,
                    Status = item.Status,
                });
            }
            return new DataResponse<List<GetContactDto>>(ResponseType.Success, dto);
        }

        public async Task<IDataResponse<IDto>> GetByIdAsync<IDto>(int id)
        {
            var data = await _uow.GetRepository<Contact>().GetByFilterAsync(x => x.Id == id);
            if (data != null)
            {
                //var dto = new GetContactDto()
                //{
                //    Id = item.Id,
                //    Title = item.Title,
                //    Content = item.Content,
                //    Adress = item.Adress,
                //    Skype = item.Skype,
                //    Mail = item.Mail,
                //    Phone = item.Phone,
                //    Status = item.Status,
                //};
                var dto = _mapper.Map<IDto>(await _uow.GetRepository<Contact>().GetByFilterAsync(x => x.Id == id));
                return new DataResponse<IDto>(ResponseType.Success, dto);
            }
            else
            {
                return new DataResponse<IDto>(ResponseType.NotFound, $"{id} {ContactMessages.NotFoundIdContact}");
            }
        }

        public async Task<IDataResponse<CreateContactDto>> InsertAsync(CreateContactDto createContactDto)
        {
            var validationResult = _createContactDtoValidator.Validate(createContactDto);
            if (validationResult.IsValid)
            {
                var entity = new Contact()
                {
                    Title = createContactDto.Title,
                    Content = createContactDto.Content,
                    Adress = createContactDto.Adress,
                    Skype = createContactDto.Skype,
                    Mail = createContactDto.Mail,
                    Phone = createContactDto.Phone,
                    Status = createContactDto.Status,
                };
                await _uow.GetRepository<Contact>().InsertAsync(entity);
                await _uow.SaveChangesAsync();
                return new DataResponse<CreateContactDto>(ResponseType.Success, createContactDto);
            }
            else
            {
                return new DataResponse<CreateContactDto>(ResponseType.ValidationError, createContactDto, validationResult.ConverToCustomValidationError());
            }
        }

        public async Task<IDataResponse<UpdateContactDto>> UpdateAsync(UpdateContactDto updateContactDto)
        {
            var validationResult = _updateContactDtoValidator.Validate(updateContactDto);
            if (validationResult.IsValid)
            {
                var unChangedData = await _uow.GetRepository<Contact>().GetByIdAsync(updateContactDto.Id);
                if (unChangedData != null)
                {
                    _uow.GetRepository<Contact>().Update(new()
                    {
                        Id = updateContactDto.Id,
                        Title = updateContactDto.Title,
                        Content = updateContactDto.Content,
                        Adress = updateContactDto.Adress,
                        Skype = updateContactDto.Skype,
                        Mail = updateContactDto.Mail,
                        Phone = updateContactDto.Phone,
                        Status = updateContactDto.Status,
                    }, unChangedData);
                    await _uow.SaveChangesAsync();
                    return new DataResponse<UpdateContactDto>(ResponseType.Success, updateContactDto);
                }
                return new DataResponse<UpdateContactDto>(ResponseType.NotFound, ContactMessages.NotFoundContact);
            }
            else
            {
                return new DataResponse<UpdateContactDto>(ResponseType.ValidationError, updateContactDto, validationResult.ConverToCustomValidationError());
            }
        }
    }
}
