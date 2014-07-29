using System;

namespace EVE.Core
{
    public interface IDataService<T>
    {
        void GetData(Action<T, Exception> callback);
    }
}