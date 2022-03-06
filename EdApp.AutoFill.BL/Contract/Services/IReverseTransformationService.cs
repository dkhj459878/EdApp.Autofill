using System.Collections.Generic;
using EdApp.AutoFill.BL.Model;

namespace EdApp.AutoFill.BL.Contract.Services;

public interface IReverseTransformationService
{
    string TransformReversely(IReadOnlyCollection<ParameterDto> parameterDtos,
        IReadOnlyCollection<AttributeDto> attributeDtos);

}