using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KRR.Logic
{
    class State
    {
        public List<Fluent> Fluents;
        public State()
        {
            Fluents = new List<Fluent>();
        }

        public void addFluent(Fluent fluent)
        {
            Fluents.Add(fluent);
        }

        public void changeFluent(Fluent fluent)
        {
            foreach(Fluent item in Fluents)
            {
                if (item.Name.Equals(fluent.Name))
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
            return this;
        }
        public bool check(Fluent fluent)
        {
            foreach (Fluent item in Fluents)
            {
                if (item.Name.Equals(fluent.Name))
                {
                    if(item.IsTrue != fluent.IsTrue)
                        return false;
                    break;
                }
            }
            return true;
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

        public string toString()
        {
            string result = "";
            foreach(Fluent fluent in Fluents)
            {
                result +=" | "+ fluent.Name + " - " + fluent.IsTrue;
            }
            return result;
        }
      

    }
}
