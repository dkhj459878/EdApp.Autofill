using System.Collections.Generic;
using EdApp.AutoFill.BL.Model;

namespace EdApp.AutoFill.BL.Contract.Services
{
    public interface IJsonDataLoaderService
    {
        ICollection<AttributeDto> GetJsonDataConvertedToObject(string fullFilePath);
    }
}
