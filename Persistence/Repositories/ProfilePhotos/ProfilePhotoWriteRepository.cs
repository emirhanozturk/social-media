using Application.Repositories.ProfilePhotos;
using Domain.Entities;
using Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories.ProfilePhotos
{
    public class ProfilePhotoWriteRepository : WriteRepository<ProfilePhoto>, IProfilePhotoWriteRepository
    {
        public ProfilePhotoWriteRepository(BaseDbContext context) : base(context)
        {
        }
    }
}
