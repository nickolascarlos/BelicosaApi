using Microsoft.EntityFrameworkCore;

namespace BelicosaApi.BusinessLogic
{
    public abstract class ContextServiceBase<T> where T : DbContext
    {
        protected readonly T _context;
        
        protected ContextServiceBase(T context)
        {
            _context = context;
        }
    }
}
