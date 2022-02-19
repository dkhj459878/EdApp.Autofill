using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using EdApp.AutoFill.BL.Contract;
using EdApp.AutoFill.BL.Enums;
using EdApp.AutoFill.BL.Model;

namespace EdApp.AutoFill.BL.Extensions;

public static class MapTypeToExcelExtensions
{
    public static Dictionary<PropertyInfo, (int Index, bool IsPrimaryForCheckingOnExistance)> GetMap<TSource>() where TSource : ModelDtoBase<TSource>, new()
    {
        return typeof(TSource).Name switch
        {
            nameof(AttributeDto) => GetAttributeDtoMapTypeToExcel(),
            nameof(ParameterDto) => GetParameterDtoMapTypeToExcel(),
            _ => throw new ArgumentOutOfRangeException($"Handling {typeof(TSource).Name} is not provided.")
        };
    }

    private static Dictionary<PropertyInfo, (int Index, bool IsPrimaryForCheckingOnExistance)> GetAttributeDtoMapTypeToExcel()
    {
        var mapTypeToExcelBuilder = new MapTypeToExcelBuilder();
        mapTypeToExcelBuilder
            .MapPropertyToExcelColumn<AttributeDto, string>(x => x.Name, (int)AttributeDtoColumnIndex.Name)
            .MapPropertyToExcelColumn<AttributeDto, string>(x => x.Value, (int)AttributeDtoColumnIndex.Value)
            .MapPropertyToExcelColumn<AttributeDto, string>(x => x.Unit, (int)AttributeDtoColumnIndex.Unit)
            .MapPropertyToExcelColumn<AttributeDto, string>(x => x.Description,
                (int)AttributeDtoColumnIndex.Description);

        return mapTypeToExcelBuilder.Build();
    }

    private static Dictionary<PropertyInfo, (int Index, bool IsPrimaryForCheckingOnExistance)> GetParameterDtoMapTypeToExcel()
    {
        var mapTypeToExcelBuilder = new MapTypeToExcelBuilder();
        mapTypeToExcelBuilder
            .MapPropertyToExcelColumn((ParameterDto x) => x.Name, (int) ParameterDtoExcelColumnIndex.Name)
            .MapPropertyToExcelColumn((ParameterDto x) => x.ParentEntity, (int) ParameterDtoExcelColumnIndex.ParentEntity)
            .MapPropertyToExcelColumn((ParameterDto x) => x.Field, (int) ParameterDtoExcelColumnIndex.Field)
            .MapPropertyToExcelColumn((ParameterDto x) => x.UIName, (int) ParameterDtoExcelColumnIndex.UIName)
            .MapPropertyToExcelColumn((ParameterDto x) => x.DataType, (int) ParameterDtoExcelColumnIndex.Name)
            .MapPropertyToExcelColumn((ParameterDto x) => x.Name, (int) ParameterDtoExcelColumnIndex.Name)
            .MapPropertyToExcelColumn((ParameterDto x) => x.Name, (int) ParameterDtoExcelColumnIndex.Name)
            .MapPropertyToExcelColumn((ParameterDto x) => x.Name, (int) ParameterDtoExcelColumnIndex.Name)
            .MapPropertyToExcelColumn((ParameterDto x) => x.Name, (int) ParameterDtoExcelColumnIndex.Name)
            .MapPropertyToExcelColumn((ParameterDto x) => x.Name, (int) ParameterDtoExcelColumnIndex.Name)
            .MapPropertyToExcelColumn((ParameterDto x) => x.Name, (int) ParameterDtoExcelColumnIndex.Name)
            .MapPropertyToExcelColumn((ParameterDto x) => x.Name, (int) ParameterDtoExcelColumnIndex.Name)
            .MapPropertyToExcelColumn((ParameterDto x) => x.Name, (int) ParameterDtoExcelColumnIndex.Name)
            .MapPropertyToExcelColumn((ParameterDto x) => x.Name, (int) ParameterDtoExcelColumnIndex.Name)
            .MapPropertyToExcelColumn((ParameterDto x) => x.Name, (int) ParameterDtoExcelColumnIndex.Name)
            .MapPropertyToExcelColumn((ParameterDto x) => x.Name, (int) ParameterDtoExcelColumnIndex.Name)
            .MapPropertyToExcelColumn((ParameterDto x) => x.Name, (int) ParameterDtoExcelColumnIndex.Name)
            .MapPropertyToExcelColumn((ParameterDto x) => x.Name, (int) ParameterDtoExcelColumnIndex.Name)
            .MapPropertyToExcelColumn((ParameterDto x) => x.Name, (int) ParameterDtoExcelColumnIndex.Name)
            .MapPropertyToExcelColumn((ParameterDto x) => x.Name, (int) ParameterDtoExcelColumnIndex.Name)
            .MapPropertyToExcelColumn((ParameterDto x) => x.Name, (int) ParameterDtoExcelColumnIndex.Name)
            .MapPropertyToExcelColumn((ParameterDto x) => x.Name, (int) ParameterDtoExcelColumnIndex.Name)
            .MapPropertyToExcelColumn((ParameterDto x) => x.Name, (int) ParameterDtoExcelColumnIndex.Name)
            .MapPropertyToExcelColumn((ParameterDto x) => x.Name, (int) ParameterDtoExcelColumnIndex.Name)
            ;
        return mapTypeToExcelBuilder.Build();
    }

    private class MapTypeToExcelBuilder
    {
        private readonly Dictionary<PropertyInfo, (int Index, bool IsPrimaryForCheckingOnExistance)> _mapTypeToExcel;

        public MapTypeToExcelBuilder()
        {
            _mapTypeToExcel = new Dictionary<PropertyInfo, (int Index, bool IsPrimaryForCheckingOnExistance)>();
        }

        public MapTypeToExcelBuilder MapPropertyToExcelColumn<TSource, TProperty>(
            Expression<Func<TSource, TProperty>> propertyLambda, int columnIndex, bool isPrimaryForCheckingOnExistence = false)
        {
            MemberExpression expression = (MemberExpression)propertyLambda.Body;
            var property = (PropertyInfo)expression.Member;
            _mapTypeToExcel.Add(property, (columnIndex, isPrimaryForCheckingOnExistence));
            return this;
        }

        public Dictionary<PropertyInfo, (int Index, bool IsPrimaryForCheckingOnExistance)> Build()
        {
            return _mapTypeToExcel;
        }
    }
}