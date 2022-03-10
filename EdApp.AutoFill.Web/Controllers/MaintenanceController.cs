using System.IO;
using EdApp.AutoFill.BL.Contract.Services;
using EdApp.AutoFill.Web.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace EdApp.AutoFill.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MaintenanceController : ControllerBase
    {
        private const string DynamicTorque = "DynamicTorque";
        private const string WindingDesignRoundWire = "WindingDesignRoundWire";

        private readonly ILogger<MaintenanceController> _logger;
        private readonly ICalculationTypeService _calculationTypeService;
        private readonly IModelTypeService _modelTypeService;
        private readonly IParameterService _parameterService;
        private readonly ILoadAllDataService _loadAllDataService;
        private readonly IReverseTransformationService _reverseTransformationService;

        public MaintenanceController(ILogger<MaintenanceController> logger, ICalculationTypeService calculationTypeService, IModelTypeService modelTypeService, IParameterService parameterService, ILoadAllDataService loadAllDataService, IReverseTransformationService reverseTransformationService)
        {
            _logger = logger;
            _calculationTypeService = calculationTypeService;
            _modelTypeService = modelTypeService;
            _parameterService = parameterService;
            _loadAllDataService = loadAllDataService;
            _reverseTransformationService = reverseTransformationService;
        }

        private const string ClearData = @"https://localhost:44392/Maintenance/ClearAllDatabases";

        private const string LoadData = @"https://localhost:44392/Maintenance/LoadAllData";

        private const string ReverseJson = @"https://localhost:44392/Maintenance/ReverseJson";

        private string HtmlStartup => $@"<!doctype html><html><head>
        <title>Siemens utilities</title>
    </head>
    <body>
        <ul>
            <li><a href=""{ClearData}"">Clear all data.</a>Run its first.</li>
            <li><a href=""{LoadData}"">Load all the data.</a>Load all data. Run it's second.</li>
            <li><a href=""{ReverseJson}"">Reverse json.</a>Get json file.</li>
        </ul>
    </body>
</html>
";

        private readonly string dataType = "text/html";

        [HttpGet("Startup")]
        public ActionResult<string> Index()
        {
            return base.Content(HtmlStartup, dataType); ;
        }

        [HttpGet("ClearAllDatabases")]
        public ActionResult<string> ClearAllDatabases()
        {
            _parameterService.DeleteAllParameters();
            _calculationTypeService.DeleteAllCalculationTypes();
            _modelTypeService.DeleteAllModelTypes();
            return Ok("All databases cleared successfully.");
        }

        [HttpGet("LoadAllData")]
        public ActionResult<string> LoadAllData()
        {
            _loadAllDataService.LoadAll();
            return Ok("All data updated successfully.");
        }

        [HttpGet("ReverseJson")]
        public ActionResult<string> GetReverseJsonModel()
        {
            var json = _reverseTransformationService.TransformReversely(GetFileInfo("175939_DM.json"), DynamicTorque);
            return Ok(json);
        }

        private FileInfo GetFileInfo(string fileName)
        {
            var solutionPath = Path.GetFullPath(Path.Combine(AppHelpers.ProjectPath, @"..\..\.."));
            const string backSlash = "\\";
            var folder = string.Concat(solutionPath, backSlash, "tests\\EdApp.AutoFill.UnitTests\\Resources");
            return new FileInfo(Path.Combine(folder, fileName));
        }
    }
}
