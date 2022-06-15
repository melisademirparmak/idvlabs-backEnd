using System;

namespace IdlavbsTodoList_API
{
    public class Users
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string UserMail { get; set; }

        public string UserPassword { get; set; }

        public DateTime CreateDate { get; set; }
        public DateTime LastLogin { get; set; }
    }
}
