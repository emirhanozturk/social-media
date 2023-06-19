using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstracts.Services
{
    public interface IChatService
    {
        public bool AddUserToList(string userToAdd);
        public void AddUserConnectionId(string user, string connectionId);
        public string GetUserByConnectionId(string connectionId);
        public string GetConnectionIdByUser(string user);
        public void RemoveUserFromList(string user);
        public string[] GetOnlineUsers();



    }
}
