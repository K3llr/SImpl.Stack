namespace spike.stack.application.Domain
{
    public class SpanishGreetingService : IGreetingService
    {
        public string SayHi(string name)
        {
            return $"Hola {name}";
        }

        public string SayBye(string name)
        {
            return $"Chao {name}";
        }
    }
}