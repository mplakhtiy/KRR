using KRR.Logic.TruthTable;

namespace KRR.Logic.Rules
{
    public class Always :IRule
    {
        public string Name { get; set; }
        public Evaluator evaluator { get; set; }
        public Always (Evaluator eval)
        {
            Name = "always";
            evaluator = eval;
        }
    }
}
