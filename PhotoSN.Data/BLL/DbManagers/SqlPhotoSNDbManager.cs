using PhotoSN.Data.DAL.DbContexts;
using System;

namespace PhotoSN.Data.BLL.DbManagers
{
    public class SqlPhotoSNDbManager : IPhotoSNDbManager
    {
        public PhotoSNDbContext PhotoSNDbContext { get; set; }

        public SqlPhotoSNDbManager(PhotoSNDbContext photoSNDbContext)
        {
            PhotoSNDbContext = photoSNDbContext;
        }

        public void Dispose()
        {
            PhotoSNDbContext.Dispose();
        }

        public bool Validate()
        {
            throw new NotImplementedException();
        }
    }
}
