using Application.Repositories;
using Domain.Entities;
using Persistence.Contexts;

namespace Persistence.Repositories
{
    public class VideoReadRepository : ReadRepository<Video>, IVideoReadRepository
    {
        public VideoReadRepository(BaseDbContext context) : base(context)
        {
        }
    }
}
