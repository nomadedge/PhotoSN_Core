using System;

namespace PhotoSN.Data.BLL.DbManagers
{
    public interface IPhotoSNDbManager : IDisposable
    {
        bool Validate();
    }
}