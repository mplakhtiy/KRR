using System.Collections.Generic;
namespace KRR.Logic.Rules
{
    public class CausesIf : IRule
    {
        public string Name { get; set; }
        public List<Fluent> change { get; set; }
        public List<List<Fluent>> _if { get; set; }
        public Agent_Action agent_action { get; set; }
        public CausesIf(Agent_Action agent_action, List<Fluent> change_, List<List<Fluent>> _if_)
        {
            this.change = new List<Fluent>();
            this._if = new List<List<Fluent>>();
            foreach (Fluent fluent in change_)
            {
                this.change.Add(fluent);
            }
 
            foreach (List<Fluent> list in _if_)
            {
               
                    _if.Add(list);
             
            }
            this.Name = "causes";
            this.agent_action = agent_action;
        }
        public override string ToString()
        {
            string text= this.agent_action.agent.Name + "(" + agent_action.action.Name + ")" + " " + Name + " " ;
           /* foreach (var v in change)
            {
                text += v.Name.ToString() + " " + v.IsTrue.ToString() + ", ";
            }
            /*text += " if ";
            foreach (List<Fluent> v in _if)
            {
                foreach
            }
            */
            return text;
        }
    }
}
