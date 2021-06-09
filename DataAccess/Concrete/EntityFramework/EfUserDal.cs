using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Concrete.EntityFramework
{
    // Inherit EfEntityRepositoryBase and implement IUserDal
    public class EfUserDal : EfEntityRepositoryBase<User, UserContext>, IUserDal
    {

    }
}
