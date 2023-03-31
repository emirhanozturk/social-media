using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstracts.Hubs
{
    public interface IPostHubService
    {
        Task PostAddedMessageAsync(string message);
    }
}
