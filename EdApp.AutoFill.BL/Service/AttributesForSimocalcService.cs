using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using EdApp.AutoFill.BL.Contract.Services;
using EdApp.AutoFill.BL.Model;
using EdApp.AutoFill.DAL.Contract.Repository;
using EdApp.AutoFill.DAL.Model;

namespace EdApp.AutoFill.BL.Service
{
    /// <inheritdoc cref="IAttributesForSimocalcService" />
    public class AttributesForSimocalcService : BaseService, IAttributesForSimocalcService
    {
        private readonly IMapper _mapper;

        public AttributesForSimocalcService(IUnitOfWork unitOfWork, IMapper mapper)
            : base(unitOfWork)
        {
            _mapper = mapper;
        }

        public IEnumerable<AttributesForSimocalcDto> GetAllAttributesForSimocalcs(
            Expression<Func<AttributesForSimocalcDto, bool>> filterDto = null,
            Func<IQueryable<AttributesForSimocalcDto>, IOrderedQueryable<AttributesForSimocalcDto>> orderByDto = null,
            string includeProperties = "")
        {
            var filter =
                _mapper.Map<Expression<Func<AttributesForSimocalcDto, bool>>, Expression<Func<AttributesForSimocalc, bool>>>(
                    filterDto);
            var orderBy =
                _mapper
                    .Map<Func<IQueryable<AttributesForSimocalcDto>, IOrderedQueryable<AttributesForSimocalcDto>>,
                        Func<IQueryable<AttributesForSimocalc>, IOrderedQueryable<AttributesForSimocalc>>>(orderByDto);
            return _mapper.Map<IEnumerable<AttributesForSimocalc>, IEnumerable<AttributesForSimocalcDto>>(
                UnitOfWork.AttributesForSimocalc.Get(filter, orderBy, includeProperties));
        }

        public IEnumerable<AttributesForSimocalcDto> GetAttributesForSimocalcsPage(
            int pageSize,
            int pageNumber,
            Expression<Func<AttributesForSimocalcDto, bool>> filterDto = null,
            Func<IQueryable<AttributesForSimocalcDto>, IOrderedQueryable<AttributesForSimocalcDto>> orderByDto = null,
            string includeProperties = "")
        {
            var filter =
                _mapper.Map<Expression<Func<AttributesForSimocalcDto, bool>>, Expression<Func<AttributesForSimocalc, bool>>>(
                    filterDto);
            var orderBy =
                _mapper
                    .Map<Func<IQueryable<AttributesForSimocalcDto>, IOrderedQueryable<AttributesForSimocalcDto>>,
                        Func<IQueryable<AttributesForSimocalc>, IOrderedQueryable<AttributesForSimocalc>>>(orderByDto);
            return _mapper.Map<IEnumerable<AttributesForSimocalc>, IEnumerable<AttributesForSimocalcDto>>(
                UnitOfWork.AttributesForSimocalc.GetPage(pageSize, pageNumber, filter, orderBy, includeProperties));
        }

        public AttributesForSimocalcDto GetAttributesForSimocalc(int id)
        {
            return _mapper.Map<AttributesForSimocalc, AttributesForSimocalcDto>(UnitOfWork.AttributesForSimocalc.GetById(id));
        }

        public int AddAttributesForSimocalc(AttributesForSimocalcDto attributesForSimocalc)
        {
            var attribute = _mapper.Map<AttributesForSimocalcDto, AttributesForSimocalc>(attributesForSimocalc);
            UnitOfWork.AttributesForSimocalc.Insert(attribute);
            UnitOfWork.SaveChanges();
            // Detach added navigation entities as far as I have being got the error:
            // The instance of entity type cannot be tracked because another instance with the same key value for {'Id'}
            // is already being tracked. When attaching existing entities, ensure that only one entity instance with a given key value is attached.
            // Consider using 'DbContextOptionsBuilder.EnableSensitiveDataLogging' to see the conflicting key values.
            // I have found the answer at this URL: https://www.programmerall.com/article/3779294564/.
            UnitOfWork.AttributesForSimocalc.Detach(attribute);
            return attribute.Id;
        }

        public void UpdateAttributesForSimocalc(AttributesForSimocalcDto attributesForSimocalc)
        {
            var attribute = _mapper.Map<AttributesForSimocalcDto, AttributesForSimocalc>(attributesForSimocalc);
            UnitOfWork.AttributesForSimocalc.UpdateSimpleProperties(attribute, attribute.Id);
            UnitOfWork.SaveChanges();
        }

        public void DeleteAttributesForSimocalc(int id)
        {
            UnitOfWork.AttributesForSimocalc.Delete(id);
            UnitOfWork.SaveChanges();
        }

        public void DeleteAllAttributesForSimocalcs()
        {
            UnitOfWork.AttributesForSimocalc.DeleteAll();
            UnitOfWork.SaveChanges();
        }

        public bool AttributesForSimocalcExists(Expression<Func<AttributesForSimocalcDto, bool>> filterDto)
        {
            var filter =
                _mapper.Map<Expression<Func<AttributesForSimocalcDto, bool>>, Expression<Func<AttributesForSimocalc, bool>>>(
                    filterDto);
            return UnitOfWork.AttributesForSimocalc.Get(filter)
                .FirstOrDefault() != null;
        }
    }
}