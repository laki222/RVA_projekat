using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Model
{
    public class Racun : ICloneable
    {
        private int stanje;
        private int racunID;

        public int Stanje { get; set; }
        public int RacunID { get; set; }

        public Proizvod Proizvod { get; set; }



        public ICloneable Clone()
        {
            Racun r = new Racun();
            r.RacunID = RacunID;
            r.Stanje = Stanje;
            r.Proizvod = Proizvod;





            return r;
        }
    }
}
