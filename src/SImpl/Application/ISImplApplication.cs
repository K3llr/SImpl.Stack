using System.Threading.Tasks;

namespace SImpl.Application
{
    public interface ISImplApplication
    {
        Task StartAsync();
        
        Task StopAsync();
    }
}