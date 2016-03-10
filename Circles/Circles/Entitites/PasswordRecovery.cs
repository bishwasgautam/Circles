using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Circles.Entitites
{
    public class PasswordRecovery
    {
        //<TODO> Auto Generated, finite number of questions
        public int ID { get; set; }

        //TODO Just need the id, question can be loaded lazily
        public string Question { get; set; }
        public string Answer { get; set; }

    }
}

