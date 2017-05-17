using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KRR.Logic
{
    public class Fluent
    {
        private string Name { get; set; }
        private bool IsTrue { get; set; }

        public Fluent(string name)
        {
            this.Name = name;
            this.IsTrue = true;
        }

        public string toString()
        {
            return Name;
        }


    }
}
