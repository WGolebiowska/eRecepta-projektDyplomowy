using System.Collections.Generic;

namespace eRecepta_projektDyplomowy.ViewModels
{
    public class PageUsersModel
    {
        public int TotalUsers { get; set; }
        public List<UserModel> Users { get; set; }
            = new List<UserModel>();
    }
}
