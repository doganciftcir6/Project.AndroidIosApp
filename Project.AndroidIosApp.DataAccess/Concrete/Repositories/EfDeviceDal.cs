using Microsoft.EntityFrameworkCore;
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
    public class EfDeviceDal : Repository<Device>, IDeviceDal
    {

        public EfDeviceDal(AndroidIosContext context) : base(context)
        {
        }

        public async Task<List<Device>> GetAllWithOSAndDeviceTypeAsync()
        {
           return await _context.Set<Device>().Include(x => x.OS).Include(x => x.DeviceType).ToListAsync();
        }
    }
}
