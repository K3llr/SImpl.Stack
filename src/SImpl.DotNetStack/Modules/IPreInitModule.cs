namespace Novicell.DotNetStack.Modules
{
    public interface IPreInitModule : IDotNetStackModule
    {
        void PreInit();
    }
}