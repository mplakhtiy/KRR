using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KRR.Logic
{
    class Agent_Action
    {
        public Agent agent { get; set; }
        public Action action { get; set; }
        public Agent_Action(Agent agent, Action action)
        {
            this.action = action;
            this.agent = agent;
        }
        public bool checkIfPossible()
        {
            return agent.canPerformAction(action);
        }

    }
}
