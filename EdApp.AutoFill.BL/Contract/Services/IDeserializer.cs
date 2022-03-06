using System.IO;
using System.Text.Json;

namespace EdApp.AutoFill.BL.Contract.Services;

public interface IDeserializer
{
    T DeserializeTo<T>(FileInfo fullFilePath, JsonSerializerOptions options = null);
}