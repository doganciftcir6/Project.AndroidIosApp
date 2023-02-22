using Project.AndroidIosApp.DataAccess.Abstract.Repositories;
using Project.AndroidIosApp.DataAccess.Contexts.EntityFramework;
using Project.AndroidIosApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.AndroidIosApp.DataAccess.Concrete.Repositories
{
    public class EfSupportUserSupportDal : Repository<SupportUserSupport>, ISupportUserSupportDal
    {
        public EfSupportUserSupportDal(AndroidIosContext context) : base(context)
        {
        }
    }
}
