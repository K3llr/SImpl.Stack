using Novicell.App.AppBuilders;

namespace spike.stack.module
{
    public static class AppBuilderExtension
    {
        public static void UseTest01Module(this INovicellAppBuilder novicellAppBuilder)
        {
            var existingModule = novicellAppBuilder.GetModule<Test01Module>();
            if (existingModule is null)
            {
                novicellAppBuilder.AttachModule(new Test01Module());
            }
        }
        
        public static void UseTest02Module(this INovicellAppBuilder novicellAppBuilder)
        {
            var existingModule = novicellAppBuilder.GetModule<Test02Module>();
            if (existingModule is null)
            {
                novicellAppBuilder.AttachModule(new Test02Module());
            }
        }
    }
}