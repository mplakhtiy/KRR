using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KRR.Logic
{
    public class Agent
    {
        public string Name { get; set; }
        public List<Action> Actions;
        public Agent(string name)
        {
            this.Name = name;
            this.Actions = new List<Action>();
        }

        public void addAction(Action action)
        {
            Actions.Add(action);
        }
        public bool canPerformAction(Action action)
        {
            foreach (Action item in Actions){
                if (action.isEqual(item))
                    return true;
            }
            return false;
        }
        public void PerformAction()
        {

        }
        public string toString()
        {
            return Name;
        }
    }
}
