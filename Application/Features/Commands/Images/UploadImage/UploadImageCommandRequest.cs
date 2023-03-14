using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Commands.Images.UploadImage
{
    public class UploadImageCommandRequest : IRequest<UploadImageCommandResponse>
    {
        public string Id { get; set; }
        public IFormFileCollection? FormFiles { get; set; }
    }
}
