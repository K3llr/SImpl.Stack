namespace SImpl.Storage.Dapper.Helper
{
    internal enum QueryCacheType
    {
        Get,
        GetByMultipleIds,
        GetAll,
        Project,
        ProjectAll,
        Count,
        Insert,
        Update,
        Delete,
        DeleteAll,
        Any,
    }
}