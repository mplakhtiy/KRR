using KRR.Logic.TruthTable;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KRR.Logic.Rules
{
    public class CausesIf : IRule
    {
        public string Name { get; set; }
        public Evaluator evaluator { get; set; }
        public Evaluator littleEvaluator { get; set; }

        public List<Fluent> change { get; set; }
        public List<List<Fluent>> _if { get; set; }
        public Agent_Action agent_action { get; set; }
        public CausesIf(Evaluator eval, Agent_Action agent_action, List<Fluent> change_, List<List<Fluent>> _if_)
        {
            this.evaluator = eval;
            this.littleEvaluator = eval;
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

        public  String ReplaceFluentsWithChar(Dictionary<string, char> dict, string query)
        {

            foreach (KeyValuePair<string, char> pair in dict)
            {
                query = query.Replace(pair.Key.ToString(), pair.Value.ToString());
            }

            return query;
        }

        public  Dictionary<string, char> ConvertFluentsToChar(string inputText)
        {

            Dictionary<string, char> dict = new Dictionary<string, char>();

            List<char> lstChar = new List<char>()
                        //{'a','b','c','d','e','f', 'g','h', 'i','j','k','l','m','n','o'};
            { 'α', 'β', 'γ', 'δ', 'ε', 'ζ', 'η', 'θ', 'ι', 'κ', 'λ', 'μ', 'ν', 'ξ', 'ο', 'π', 'ρ', 'σ', 'τ', 'υ', 'φ', 'χ', 'ψ', 'ω'};


            string fluent = "";

            for (int i = 0; i < inputText.Length; i++)
            {
                if (Char.IsLetter(inputText[i]) == true && i != inputText.Length - 1)
                {
                    fluent += inputText[i];
                }
                else if (fluent != "" && (Evaluator.prec.Contains(inputText[i]) == true || inputText[i] == ' '))
                {
                    if (!dict.ContainsKey(fluent))
                    {
                        dict.Add(fluent, lstChar[0]);
                        fluent = "";
                        lstChar.RemoveAt(0);
                    }
                    else
                    {
                        fluent = "";
                    }

                }

                else if (Char.IsLetter(inputText[i]) == true && i == inputText.Length - 1)
                {
                    fluent += inputText[i];
                    if (!dict.ContainsKey(fluent))
                    {
                        dict.Add(fluent, lstChar[0]);
                    }
                }

                else if (i == inputText.Length - 1)
                {
                    if (!dict.ContainsKey(fluent))
                    {
                        fluent += inputText[i];
                        dict.Add(fluent, lstChar[0]);
                    }
                }
            }

            return dict;

        }

        private string FormatInput(string inputText)
        {
            //Replacing all the dummy operators with their actual symbols
            inputText = inputText.Replace('~', '¬');
            inputText = inputText.Replace('|', '∨');
            inputText = inputText.Replace('&', '∧');
            inputText = inputText.Replace('-', '↔');
            inputText = inputText.Replace('>', '→');

            for (int i = 0; i < inputText.Length; i++)
            {
                if (Char.IsLetter(inputText[i]) == true || Evaluator.prec.Contains(inputText[i]) == true || inputText[i] == ' ') { }
                else { inputText = inputText.Remove(i); }
            }
            return inputText;
        }

        public void checngeCauses(string eval)
        {
            if (eval != "")
            {
               // littleEvaluator.EvaluateQuery();
                string query = "(" +eval +")"+ "∧" + this.evaluator.Original;
                query = FormatInput(query);
                Dictionary<string, char> dict = ConvertFluentsToChar(query);
                String convertedText = ReplaceFluentsWithChar(dict, query);

                this.evaluator = new Evaluator(convertedText, eval);
                this.evaluator.FindEvalPlan();
                this.evaluator.EvaluateQuery(dict);
            }
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
