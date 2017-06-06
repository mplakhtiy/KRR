using System.Collections.Generic;

namespace KRR.Logic
{
    public class Agent
    {
        public string Name { get; set; }
        public List<Action> Actions; //only allowed //adnrew said we need to delete this 
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
            foreach (Action item in Actions)
            {
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
