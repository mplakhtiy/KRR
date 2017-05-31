using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KRR.Logic.Rules
{
    class ReleasesIf:IRule
    {
        public double probability;
        public string Name { get; set; }
        public List<Fluent> change { get; set; }
        public List<List<Fluent>> _if { get; set; }
        public Agent_Action agent_action { get; set; }
        public ReleasesIf(Agent agent, Action action, List<Fluent> change, List<Fluent> _if )
        {
            this.change = change;
            this._if = _if;
            this.agent_action = agent_action;
            this.probability = 0.5;
        }
        
    }
}
