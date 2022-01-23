using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using EdApp.AutoFill.BL.Contract.Services;
using EdApp.AutoFill.BL.Model;
using EdApp.AutoFill.DAL.Contract.Repository;

namespace EdApp.AutoFill.BL.Service
{
    /// <inheritdoc cref="IAttributeDtoService" />
    public class AttributeDtoService : BaseService, IAttributeDtoService
    {
        private readonly IMapper _mapper;

        public AttributeDtoService(IUnitOfWork unitOfWork, IMapper mapper)
            : base(unitOfWork)
        {
            _mapper = mapper;
        }

        public IEnumerable<AttributeDto> GetAllAttributeDtos(
            Expression<Func<AttributeDto, bool>> filterDto = null,
            Func<IQueryable<AttributeDto>, IOrderedQueryable<AttributeDto>> orderByDto = null,
            string includeProperties = "")
        {
            var filter =
                _mapper.Map<Expression<Func<AttributeDto, bool>>, Expression<Func<AttributeDto, bool>>>(
                    filterDto);
            var orderBy =
                _mapper
                    .Map<Func<IQueryable<AttributeDto>, IOrderedQueryable<AttributeDto>>,
                        Func<IQueryable<AttributeDto>, IOrderedQueryable<AttributeDto>>>(orderByDto);
            return _mapper.Map<IEnumerable<AttributeDto>, IEnumerable<AttributeDto>>(
                UnitOfWork.AttributeDto.Get(filter, orderBy, includeProperties));
        }

        public IEnumerable<AttributeDto> GetAttributeDtosPage(
            int pageSize,
            int pageNumber,
            Expression<Func<AttributeDto, bool>> filterDto = null,
            Func<IQueryable<AttributeDto>, IOrderedQueryable<AttributeDto>> orderByDto = null,
            string includeProperties = "")
        {
            var filter =
                _mapper.Map<Expression<Func<AttributeDto, bool>>, Expression<Func<AttributeDto, bool>>>(
                    filterDto);
            var orderBy =
                _mapper
                    .Map<Func<IQueryable<AttributeDto>, IOrderedQueryable<AttributeDto>>,
                        Func<IQueryable<AttributeDto>, IOrderedQueryable<AttributeDto>>>(orderByDto);
            return _mapper.Map<IEnumerable<AttributeDto>, IEnumerable<AttributeDto>>(
                UnitOfWork.AttributeDto.GetPage(pageSize, pageNumber, filter, orderBy, includeProperties));
        }

        public AttributeDto GetAttributeDto(int id)
        {
            return _mapper.Map<AttributeDto, AttributeDto>(UnitOfWork.AttributeDto.GetById(id));
        }

        public int AddAttributeDto(AttributeDto attributeDto)
        {
            var attribute = _mapper.Map<AttributeDto, AttributeDto>(attributeDto);
            UnitOfWork.AttributeDto.Insert(attribute);
            UnitOfWork.SaveChanges();
            // Detach added navigation entities as far as I have being got the error:
            // The instance of entity type cannot be tracked because another instance with the same key value for {'Id'}
            // is already being tracked. When attaching existing entities, ensure that only one entity instance with a given key value is attached.
            // Consider using 'DbContextOptionsBuilder.EnableSensitiveDataLogging' to see the conflicting key values.
            // I have found the answer at this URL: https://www.programmerall.com/article/3779294564/.
            UnitOfWork.AttributeDto.Detach(attribute);
            return attribute.Id;
        }

        public void UpdateAttributeDto(AttributeDto attributeDto)
        {
            var attribute = _mapper.Map<AttributeDto, AttributeDto>(attributeDto);
            UnitOfWork.AttributeDto.UpdateSimpleProperties(attribute, attribute.Id);
            UnitOfWork.SaveChanges();
        }

        public void DeleteAttributeDto(int id)
        {
            UnitOfWork.AttributeDto.Delete(id);
            UnitOfWork.SaveChanges();
        }

        public void DeleteAllAttributeDtos()
        {
            UnitOfWork.AttributeDto.DeleteAll();
            UnitOfWork.SaveChanges();
        }

        public bool AttributeDtoExists(Expression<Func<AttributeDto, bool>> filterDto)
        {
            var filter =
                _mapper.Map<Expression<Func<AttributeDto, bool>>, Expression<Func<AttributeDto, bool>>>(
                    filterDto);
            return UnitOfWork.AttributeDto.Get(filter)
                .FirstOrDefault() != null;
        }
    }
}