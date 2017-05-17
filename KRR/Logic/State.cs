using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KRR.Logic
{
    class State
    {
        private List<Fluent> Fluents;
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
        public void changeList(List<Fluent> list)
        {
            foreach (Fluent fluent in list)
            {
                changeFluent(fluent);
            }
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
        public bool checkList(List<Fluent> list)
        {
            foreach (Fluent fluent in list)
            {
                if (!check(fluent))
                    return false;
            }
            return true;
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
