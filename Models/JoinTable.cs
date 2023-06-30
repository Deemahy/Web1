using Microsoft.EntityFrameworkCore;

namespace Web1.Models
{
    public class JoinTable
    {
        public  Appontment JTappontment { get; set; }

        public  AvailabiltyDate JTavailabiltyDate { get; set; }

        public  Clinic JTclinic { get; set; }

        public  Doctor JTdoctor { get; set; }

        public  Emergency JTemergencie  { get; set; }

        public  LogIn JTlogIn { get; set; }

        public  Manegeer JTmanegeer { get; set; }

        public  NightShift JTnightShift { get; set; }

        public  PatentVacc JTpatentVacc { get; set; }

        public  Patient JTpatient { get; set; }

        public  Reseption JTreseption { get; set; }

        public  Role JTrole { get; set; }

        public  User JTuser { get; set; }

        public  Vaccsine JTvaccsine { get; set; }
    }
}
