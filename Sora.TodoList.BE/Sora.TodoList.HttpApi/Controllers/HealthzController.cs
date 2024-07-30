using Microsoft.AspNetCore.Mvc;

namespace Sora.TodoList.HttpApi.Controllers
{
    [Route("api/Healthz")]
    [ApiController]
    public class HealthzController : ControllerBase
    {
        #region Khởi tạo

        public HealthzController()
        {
        }

        #endregion Khởi tạo

        [HttpGet]
        public string Index()
        {
            return "Healthy";
        }
    }
}