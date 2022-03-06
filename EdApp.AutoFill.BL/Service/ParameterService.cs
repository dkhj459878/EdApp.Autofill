using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using EdApp.AutoFill.BL.Contract.Services;
using EdApp.AutoFill.BL.Model;
using EdApp.AutoFill.DAL.Contract.Repository;
using EdApp.AutoFill.DAL.Model;

namespace EdApp.AutoFill.BL.Service;

/// <inheritdoc cref="IParameterService" />
public class ParameterService : BaseService, IParameterService
{
    private readonly IMapper _mapper;

    public ParameterService(IUnitOfWork unitOfWork, IMapper mapper)
        : base(unitOfWork)
    {
        _mapper = mapper;
    }

    public IEnumerable<ParameterDto> GetAllParameters(
        Expression<Func<ParameterDto, bool>> filterDto = null,
        Func<IQueryable<ParameterDto>, IOrderedQueryable<ParameterDto>> orderByDto = null,
        string includeProperties = "")
    {
        var filter =
            _mapper.Map<Expression<Func<ParameterDto, bool>>, Expression<Func<Parameter, bool>>>(filterDto);
        var orderBy =
            _mapper
                .Map<Func<IQueryable<ParameterDto>, IOrderedQueryable<ParameterDto>>,
                    Func<IQueryable<Parameter>, IOrderedQueryable<Parameter>>>(orderByDto);
        return _mapper.Map<IEnumerable<Parameter>, IEnumerable<ParameterDto>>(
            UnitOfWork.Parameter.Get(filter, orderBy, includeProperties));
    }

    public IEnumerable<ParameterDto> GetParametersPage(
        int pageSize,
        int pageNumber,
        Expression<Func<ParameterDto, bool>> filterDto = null,
        Func<IQueryable<ParameterDto>, IOrderedQueryable<ParameterDto>> orderByDto = null,
        string includeProperties = "")
    {
        var filter =
            _mapper.Map<Expression<Func<ParameterDto, bool>>, Expression<Func<Parameter, bool>>>(filterDto);
        var orderBy =
            _mapper
                .Map<Func<IQueryable<ParameterDto>, IOrderedQueryable<ParameterDto>>,
                    Func<IQueryable<Parameter>, IOrderedQueryable<Parameter>>>(orderByDto);
        return _mapper.Map<IEnumerable<Parameter>, IEnumerable<ParameterDto>>(
            UnitOfWork.Parameter.GetPage(pageSize, pageNumber, filter, orderBy, includeProperties));
    }

    public ParameterDto GetParameter(int id)
    {
        return _mapper.Map<Parameter, ParameterDto>(UnitOfWork.Parameter.GetById(id));
    }

    public int AddParameter(ParameterDto parameterDto)
    {
        var parameter = _mapper.Map<ParameterDto, Parameter>(parameterDto);
        UnitOfWork.Parameter.Insert(parameter);
        UnitOfWork.SaveChanges();

        #region Detached used navigation entities.

        // Detach added navigation entities as far as I have being got the error:
        // The instance of entity type cannot be tracked because another instance with the same key value for {'Id'}
        // is already being tracked. When attaching existing entities, ensure that only one entity instance with a given key value is attached.
        // Consider using 'DbContextOptionsBuilder.EnableSensitiveDataLogging' to see the conflicting key values.
        // I have found the answer at this URL: https://www.programmerall.com/article/3779294564/.
        UnitOfWork.CalculationType.Detach(parameter.CalculationType);
        UnitOfWork.ModelType.Detach(parameter.ModelType);

        #endregion

        return parameter.Id;
    }

    public void UpdateParameter(ParameterDto parameterDto)
    {
        var parameter = _mapper.Map<ParameterDto, Parameter>(parameterDto);
        UnitOfWork.Parameter.UpdateSimpleProperties(parameter, parameter.Id);
        UnitOfWork.SaveChanges();
    }

    public void DeleteParameter(int id)
    {
        UnitOfWork.Parameter.Delete(id);
        UnitOfWork.SaveChanges();
    }

    public void DeleteAllParameters()
    {
        UnitOfWork.Parameter.DeleteAll();
        UnitOfWork.SaveChanges();
    }

    public bool ParameterExists(Expression<Func<ParameterDto, bool>> filterDto)
    {
        var filter =
            _mapper.Map<Expression<Func<ParameterDto, bool>>, Expression<Func<Parameter, bool>>>(
                filterDto);
        return UnitOfWork.Parameter.Get(filter)
            .FirstOrDefault() != null;
    }
}