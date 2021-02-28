using System;

namespace SImpl.Common
{
    public class TypeOf
    {
        public static TypeOf<TServiceType> New<TServiceType, TImplType>()
            where TImplType : TServiceType
        {
            return new TypeOf<TServiceType>(typeof(TImplType));
        }
    }
    
    public class TypeOf<T> : TypeOf
    {
        public TypeOf(Type implType)
        {
            ImplType = implType;
        }
        
        public Type ImplType { get; }
    }
}