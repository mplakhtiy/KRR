using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KRR.Logic.Rules
{
    class ReleasesIf
    {
        public double probability;

        public List<Fluent> change { get; set; }
        public List<Fluent> _if { get; set; }
        public Agent agent { get; set; }
        public Action action { get; set; }
        public ReleasesIf(Agent agent, Action action, List<Fluent> change, List<Fluent> _if )
        {
            this.change = change;
            this._if = _if;
            this.agent = agent;
            this.action = action;
            this.probability = 0.5;
        }
        
    }
}
