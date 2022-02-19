using System.Collections.Generic;
using EdApp.AutoFill.BL.Enums;
using EdApp.AutoFill.BL.Model;

namespace EdApp.AutoFill.BL.Contract.Services
{
    public interface IJsonDataLoaderService
    {
        ICollection<AttributeDto> GetJsonDataConvertedToObject(JsonKind jsonKind = JsonKind.None);
    }
}
