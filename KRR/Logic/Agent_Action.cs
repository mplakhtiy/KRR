namespace KRR.Logic
{
    public class Agent_Action
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
        public bool isEqual(Agent_Action agentAction)
        {
            if (this.agent.Name == agentAction.agent.Name && this.action.Name == agentAction.action.Name)
                return true;
            return false;
        }
    }
}
