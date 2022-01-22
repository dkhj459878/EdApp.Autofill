using EdApp.AutoFill.BL.Contract.Services;
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
