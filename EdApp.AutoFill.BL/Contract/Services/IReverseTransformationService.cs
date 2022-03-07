using System.Collections.Generic;
using System.IO;
using EdApp.AutoFill.BL.Model;

namespace EdApp.AutoFill.BL.Contract.Services;

public interface IReverseTransformationService
{
    string TransformReversely(FileInfo fileInfo, string calculationType);

}