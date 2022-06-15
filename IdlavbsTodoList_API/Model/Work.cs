using System;

namespace IdlavbsTodoList_API
{
    public class Work : Users
    {
        public int WorkID { get; set; }

        public int UserID { get; set; }

        public string WorkName { get; set; }
        public string WorkDetail { get; set; }

        public int WorkStatus { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime FinishDate { get; set; }

        public string WorkStatusName(int workstatus)
        {
            string result = "";
            switch (workstatus)
            {
                case 0:
                    result = "Başlamadı";
                    break;
                case 1:
                    result = "Yapılıyor";
                    break;
                case 2:
                    result = "Tamamlandı";
                    break;
                case 3:
                    result = "İptal Edildi";
                    break;
            }
            return result;
        }
    }
}
