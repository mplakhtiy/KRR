using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KRR.Logic
{
    public class Fluent
    {
        public string Name;

        public Fluent(string name)
        {
            this.Name = name;

        }



        public bool IsTrue()
        {
            if (true)
            {
                return true;
            }
            return false;
        }

    }
}
