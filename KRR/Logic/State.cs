using System.Collections.Generic;

namespace KRR.Logic
{
    public class State
    {
        public List<Fluent> Fluents { get; private set; }
        public State()
        {
            Fluents = new List<Fluent>();
        }


        public State(State state)
        {
            Fluents = new List<Fluent>();
            if (state != null)
            {
                foreach (Fluent fluent in state.Fluents)
                {
                    Fluents.Add(new Fluent(fluent));
                }
            }
        }
        public void addFluent(Fluent fluent)
        {
            Fluents.Add(fluent);
        }

        public void changeFluent(Fluent fluent)
        {
            foreach (Fluent item in Fluents)
            {
                if (item.Name == fluent.Name)
                {
                    item.IsTrue = fluent.IsTrue;
                    return;
                }
            }
        }
        public State changeList(List<Fluent> list)
        {
            foreach (Fluent fluent in list)
            {
                changeFluent(fluent);
            }
            return new State(this);
        }
        public bool check(Fluent fluent)
        {
            foreach (Fluent item in Fluents)
            {
                if (item.Name == fluent.Name)
                {
                    if (item.IsTrue == fluent.IsTrue)
                        return true;
                    break;
                }
            }
            return false;
        }
        /// <summary>
        /// for "&&" STATEMENT
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public bool checkList(List<Fluent> list)
        {
            foreach (Fluent fluent in list)
            {
                if (!check(fluent))
                    return false;
            }
            return true;
        }
        /// <summary>
        /// for "||" statement
        /// </summary>
        /// <param name="lists"></param>
        /// <returns></returns>
        public bool checkOrList(List<List<Fluent>> lists)
        {
            foreach (List<Fluent> list in lists)
            {
                if (!checkList(list))
                    return false;
            }
            return true;
        }
        public bool isEqual(State item)
        {
            return this.checkList(item.Fluents);
        }

        public override string ToString()
        {
            string result = "";
            foreach (Fluent fluent in Fluents)
            {
                result += " | " + fluent.Name + " - " + fluent.IsTrue;
            }
            return result;
        }


    }
}
