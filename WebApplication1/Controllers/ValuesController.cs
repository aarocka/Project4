using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using RestaurantReviewLibrary;
namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        [HttpGet]
        public UserSession Get()
        {
            UserSession userSession = new UserSession("Kevin", true);
            return userSession;
        }
    }
}
