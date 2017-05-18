using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KRR.Logic
{
    public class Action
    {
        public string Name { get; set; }
        public Action(string Name) {
            this.Name = Name;
        }
        public bool isEqual (Action action)
        {
            if (action.Name.Equals(this.Name))
                return true;
            return false;
        }
        public string toString()
        {
            return Name;
        }

    }
}
