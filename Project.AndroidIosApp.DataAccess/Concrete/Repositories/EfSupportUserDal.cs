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
    public class EfSupportUserDal : Repository<SupportUser>, ISupportUserDal
    {
        public EfSupportUserDal(AndroidIosContext context) : base(context)
        {
        }
    }
}
