using System.IO;

namespace EdApp.AutoFill.BL.Contract.Services;

public interface IReverseTransformationService
{
    string TransformReversely(FileInfo fileInfo, string calculationType);

}