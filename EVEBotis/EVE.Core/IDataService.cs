using System;

namespace ILoveEVE.Core
{
    public interface IDataService<T>
    {
        void GetData(Action<T, Exception> callback);
    }
}