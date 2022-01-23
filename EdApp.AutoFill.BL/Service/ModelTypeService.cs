using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using EdApp.AutoFill.BL.Contract.Services;
using EdApp.AutoFill.DAL.Contract.Repository;
using EdApp.AutoFill.DAL.Model;

namespace EdApp.AutoFill.BL.Service
{
    /// <inheritdoc cref="IModelTypeService" />
    public class ModelTypeService : BaseService, IModelTypeService
    {
        private readonly IMapper _mapper;

        public ModelTypeService(IUnitOfWork unitOfWork, IMapper mapper)
            : base(unitOfWork)
        {
            _mapper = mapper;
        }

        public IEnumerable<ModelTypeDto> GetAllModelTypes(
            Expression<Func<ModelTypeDto, bool>> filterDto = null,
            Func<IQueryable<ModelTypeDto>, IOrderedQueryable<ModelTypeDto>> orderByDto = null,
            string includeProperties = "")
        {
            var filter =
                _mapper.Map<Expression<Func<ModelTypeDto, bool>>, Expression<Func<ModelType, bool>>>(filterDto);
            var orderBy =
                _mapper
                    .Map<Func<IQueryable<ModelTypeDto>, IOrderedQueryable<ModelTypeDto>>,
                        Func<IQueryable<ModelType>, IOrderedQueryable<ModelType>>>(orderByDto);
            return _mapper.Map<IEnumerable<ModelType>, IEnumerable<ModelTypeDto>>(
                UnitOfWork.ModelType.Get(filter, orderBy, includeProperties));
        }

        public IEnumerable<ModelTypeDto> GetModelTypesPage(
            int pageSize,
            int pageNumber,
            Expression<Func<ModelTypeDto, bool>> filterDto = null,
            Func<IQueryable<ModelTypeDto>, IOrderedQueryable<ModelTypeDto>> orderByDto = null,
            string includeProperties = "")
        {
            var filter =
                _mapper.Map<Expression<Func<ModelTypeDto, bool>>, Expression<Func<ModelType, bool>>>(filterDto);
            var orderBy =
                _mapper
                    .Map<Func<IQueryable<ModelTypeDto>, IOrderedQueryable<ModelTypeDto>>,
                        Func<IQueryable<ModelType>, IOrderedQueryable<ModelType>>>(orderByDto);
            return _mapper.Map<IEnumerable<ModelType>, IEnumerable<ModelTypeDto>>(
                UnitOfWork.ModelType.GetPage(pageSize, pageNumber, filter, orderBy, includeProperties));
        }

        public ModelTypeDto GetModelType(int id)
        {
            return _mapper.Map<ModelType, ModelTypeDto>(UnitOfWork.ModelType.GetById(id));
        }

        public int AddModelType(ModelTypeDto modelTypeDto)
        {
            var modelType = _mapper.Map<ModelTypeDto, ModelType>(modelTypeDto);
            UnitOfWork.ModelType.Insert(modelType);
            UnitOfWork.SaveChanges();
            // Detach added navigation entities as far as I have being got the error:
            // The instance of entity type cannot be tracked because another instance with the same key value for {'Id'}
            // is already being tracked. When attaching existing entities, ensure that only one entity instance with a given key value is attached.
            // Consider using 'DbContextOptionsBuilder.EnableSensitiveDataLogging' to see the conflicting key values.
            // I have found the answer at this URL: https://www.programmerall.com/article/3779294564/.
            UnitOfWork.ModelType.Detach(modelType);
            return modelType.Id;
        }

        public void UpdateModelType(ModelTypeDto calculationTypeDto)
        {
            var calculationType = _mapper.Map<ModelTypeDto, ModelType>(calculationTypeDto);
            UnitOfWork.ModelType.UpdateSimpleProperties(calculationType, calculationType.Id);
            UnitOfWork.SaveChanges();
        }

        public void DeleteModelType(int id)
        {
            UnitOfWork.ModelType.Delete(id);
            UnitOfWork.SaveChanges();
        }

        public void DeleteAllModelTypes()
        {
            UnitOfWork.ModelType.DeleteAll();
            UnitOfWork.SaveChanges();
        }

        public bool ModelTypeExists(Expression<Func<ModelTypeDto, bool>> filterDto)
        {
            var filter =
                _mapper.Map<Expression<Func<ModelTypeDto, bool>>, Expression<Func<ModelType, bool>>>(
                    filterDto);
            return UnitOfWork.ModelType.Get(filter)
                .FirstOrDefault() != null;
        }
    }
}