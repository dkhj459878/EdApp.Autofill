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
    /// <inheritdoc cref="ICalculationTypeService" />
    public class CalculationTypeService : BaseService, ICalculationTypeService
    {
        private readonly IMapper _mapper;

        public CalculationTypeService(IUnitOfWork unitOfWork, IMapper mapper)
            : base(unitOfWork)
        {
            _mapper = mapper;
        }

        public IEnumerable<CalculationTypeDto> GetAllCalculationTypes(
            Expression<Func<CalculationTypeDto, bool>> filterDto = null,
            Func<IQueryable<CalculationTypeDto>, IOrderedQueryable<CalculationTypeDto>> orderByDto = null,
            string includeProperties = "")
        {
            var filter =
                _mapper.Map<Expression<Func<CalculationTypeDto, bool>>, Expression<Func<CalculationType, bool>>>(
                    filterDto);
            var orderBy =
                _mapper
                    .Map<Func<IQueryable<CalculationTypeDto>, IOrderedQueryable<CalculationTypeDto>>,
                        Func<IQueryable<CalculationType>, IOrderedQueryable<CalculationType>>>(orderByDto);
            return _mapper.Map<IEnumerable<CalculationType>, IEnumerable<CalculationTypeDto>>(
                UnitOfWork.CalculationType.Get(filter, orderBy, includeProperties));
        }

        public IEnumerable<CalculationTypeDto> GetCalculationTypesPage(
            int pageSize,
            int pageNumber,
            Expression<Func<CalculationTypeDto, bool>> filterDto = null,
            Func<IQueryable<CalculationTypeDto>, IOrderedQueryable<CalculationTypeDto>> orderByDto = null,
            string includeProperties = "")
        {
            var filter =
                _mapper.Map<Expression<Func<CalculationTypeDto, bool>>, Expression<Func<CalculationType, bool>>>(
                    filterDto);
            var orderBy =
                _mapper
                    .Map<Func<IQueryable<CalculationTypeDto>, IOrderedQueryable<CalculationTypeDto>>,
                        Func<IQueryable<CalculationType>, IOrderedQueryable<CalculationType>>>(orderByDto);
            return _mapper.Map<IEnumerable<CalculationType>, IEnumerable<CalculationTypeDto>>(
                UnitOfWork.CalculationType.GetPage(pageSize, pageNumber, filter, orderBy, includeProperties));
        }

        public CalculationTypeDto GetCalculationType(int id)
        {
            return _mapper.Map<CalculationType, CalculationTypeDto>(UnitOfWork.CalculationType.GetById(id));
        }

        public int AddCalculationType(CalculationTypeDto calculationTypeDto)
        {
            CalculationType calculationType;
            try
            {
                calculationType = _mapper.Map<CalculationTypeDto, CalculationType>(calculationTypeDto);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            
            UnitOfWork.CalculationType.Insert(calculationType);
            UnitOfWork.SaveChanges();
            // Detach added navigation entities as far as I have being got the error:
            // The instance of entity type cannot be tracked because another instance with the same key value for {'Id'}
            // is already being tracked. When attaching existing entities, ensure that only one entity instance with a given key value is attached.
            // Consider using 'DbContextOptionsBuilder.EnableSensitiveDataLogging' to see the conflicting key values.
            // I have found the answer at this URL: https://www.programmerall.com/article/3779294564/.
            UnitOfWork.CalculationType.Detach(calculationType);
            return calculationType.Id;
        }

        public void UpdateCalculationType(CalculationTypeDto calculationTypeDto)
        {
            var calculationType = _mapper.Map<CalculationTypeDto, CalculationType>(calculationTypeDto);
            UnitOfWork.CalculationType.UpdateSimpleProperties(calculationType, calculationType.Id);
            UnitOfWork.SaveChanges();
        }

        public void DeleteCalculationType(int id)
        {
            UnitOfWork.CalculationType.Delete(id);
            UnitOfWork.SaveChanges();
        }

        public void DeleteAllCalculationTypes()
        {
            UnitOfWork.CalculationType.DeleteAll();
            UnitOfWork.SaveChanges();
        }

        public bool CalculationTypeExists(Expression<Func<CalculationTypeDto, bool>> filterDto)
        {
            var filter =
                _mapper.Map<Expression<Func<CalculationTypeDto, bool>>, Expression<Func<CalculationType, bool>>>(
                    filterDto);
            return UnitOfWork.CalculationType.Get(filter)
                .FirstOrDefault() != null;
        }
    }
}