using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace izo.Controllers
{
    [Authorize]
    public class _BaseController : Controller
    {
    }
}
