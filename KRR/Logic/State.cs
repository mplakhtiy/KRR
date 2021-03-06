﻿using System.Collections.Generic;

namespace KRR.Logic
{
    public class State
    {
        public Agent_Action agentAction { get; set; }
        public List<Fluent> Fluents { get; private set; }
        public string name { get; set; }
        public State()
        {
            Fluents = new List<Fluent>();
            name = "";
        }


        public State(State state)
        {
            name = state.name;
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

        public State changeFluent(Fluent fluent)
        {
            foreach (Fluent item in Fluents)
            {
                if (item.Name == fluent.Name)
                {
                    item.IsTrue = fluent.IsTrue;
                    return new State(this);
                }
            }
            return new State(this);
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
            if (fluent.Name == "-")
                return false;

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
            if (lists.Count == 0)
            {
                return true;
            }
            foreach (List<Fluent> list in lists)
            {
                if (checkList(list))
                    return true;
            }
            return false;
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
