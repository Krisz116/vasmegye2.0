using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace vasmegye2._0
{
    class SzemelyiSzam
    {
        readonly string Szam;

        public string szam => Szam;

        public SzemelyiSzam(string szam)
        {
            this.Szam = szam;


        }

        public int evSzam()
        {
            int ev = int.Parse(szam.Substring(2,2));
            ev = szam[0] == '1' || szam[0] == '2' ? 1900 + ev : 2000 + ev;

            return ev;
        }
            
       
    }
}
