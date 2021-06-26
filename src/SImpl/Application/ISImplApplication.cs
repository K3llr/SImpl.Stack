using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace SImpl.Application
{
    public interface ISImplApplication
    {
        Task StartAsync(IHost host);
        
        Task StopAsync(IHost host);
    }
}