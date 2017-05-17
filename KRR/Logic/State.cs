using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KRR.Logic
{
    class State
    {
        private List<Fluent> Fluents;
        public State()
        {
            Fluents = new List<Fluent>();
        }
        public void addFluent(Fluent fluent)
        {
            Fluents.Add(fluent);
        }

    }
}
