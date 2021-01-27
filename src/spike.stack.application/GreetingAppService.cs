using spike.stack.application.Domain;

namespace spike.stack.application
{
    public class GreetingAppService : IGreetingAppService
    {
        private readonly IGreetingService _greetingService;

        public GreetingAppService(IGreetingService greetingService)
        {
            _greetingService = greetingService;
        }
        
        public string SayHi(string name)
        {
            return _greetingService.SayHi(name);
        }

        public string SayBye(string name)
        {
            return _greetingService.SayBye(name);
        }
    }
}