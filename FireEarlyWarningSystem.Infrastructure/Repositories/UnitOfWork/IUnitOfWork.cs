using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireEarlyWarningSystem.Infrastructure.Repositories.UnitOfWork
{
    public interface IUnitOfWork
    {
        public Task<bool> CompleteAsync();
    }
}
