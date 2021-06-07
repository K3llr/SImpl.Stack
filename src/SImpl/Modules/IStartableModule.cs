using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace SImpl.Modules
{
    public interface IStartableModule : ISImplModule
    {
        Task StartAsync(IHost host);
        Task StopAsync(IHost host);
    }
}