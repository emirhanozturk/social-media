﻿using Application.Repositories;
using Domain.Entities;
using Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class VideoWriteRepository : WriteRepository<Video>, IVideoWriteRepository
    {
        public VideoWriteRepository(BaseDbContext context) : base(context)
        {
        }
    }
}
