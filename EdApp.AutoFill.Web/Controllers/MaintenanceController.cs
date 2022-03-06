using EdApp.AutoFill.BL.Contract.Services;
using EdApp.AutoFill.BL.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace EdApp.AutoFill.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MaintenanceController : ControllerBase
    {
        private readonly ILogger<MaintenanceController> _logger;
        private readonly ICalculationTypeService _calculationTypeService;
        private readonly IModelTypeService _modelTypeService;
        private readonly IParameterService _parameterService;
        private readonly ILoadAllDataService _loadAllDataService;

        public MaintenanceController(ILogger<MaintenanceController> logger, ICalculationTypeService calculationTypeService, IModelTypeService modelTypeService, IParameterService parameterService, ILoadAllDataService loadAllDataService)
        {
            _logger = logger;
            _calculationTypeService = calculationTypeService;
            _modelTypeService = modelTypeService;
            _parameterService = parameterService;
            _loadAllDataService = loadAllDataService;
        }

        private string clearData = @"https://localhost:44392/Maintenance/ClearAllDatabases";

        private string loadData = @"https://localhost:44392/Maintenance/LoadAllData";

        private string HtmlStartup => $@"<!doctype html><html><head>
        <title>Siemens utilities</title>
    </head>
    <body>
        <ul>
            <li><a href=""{clearData}"">Clear all data.</a></li>
            <li><a href=""{loadData}"">Load all the data.</a></li>
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
    }
}
