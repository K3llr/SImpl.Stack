using Microsoft.AspNetCore.Mvc;
using SImpl.Http.Ping.Module;

namespace SImpl.Http.Ping.AspNetCore;

public class PingController : ControllerBase
{
    [HttpGet]
    public ActionResult Ping()
    {
        return Ok($"{DateTime.Now} | {Environment.MachineName} | {PingModule.ModuleConfig.Version} > pong");
    }
}