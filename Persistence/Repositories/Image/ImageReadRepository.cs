using Application.Repositories;
using Domain.Entities;
using Persistence.Contexts;

namespace Persistence.Repositories
{
    public class ImageReadRepository : ReadRepository<Image>, IImageReadRepository
    {
        public ImageReadRepository(BaseDbContext context) : base(context)
        {
        }
    }
}
