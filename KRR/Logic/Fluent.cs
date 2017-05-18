using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KRR.Logic
{
    public class Fluent
    {
        public string Name { get; set; }
        public bool IsTrue { get; set; }

        public Fluent(string name, bool isTrue)
        {
            this.Name = name;
            this.IsTrue = isTrue;
        }

        public string toString()
        {
            return Name;
        }


    }
}
