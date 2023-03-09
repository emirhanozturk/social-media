using Application.Repositories;
using Persistence.Contexts;

namespace Persistence.Repositories.File
{
    public class FileReadRepository : ReadRepository<Domain.Entities.File>, IFileReadRepository
    {
        public FileReadRepository(BaseDbContext context) : base(context)
        {
        }
    }
}
