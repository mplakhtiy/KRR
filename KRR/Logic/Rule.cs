using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KRR.Logic.Rules;

namespace KRR.Logic
{
    class Rule
    {
        private List<CausesIf> causesIfRules;
        private List<ReleasesIf> releasesIfRules;
        private List<Always> alwaysRules;
        private List<Impossible> impossibleRules;
        private List<Observable> observableRules;

        public Rule()
        {
            causesIfRules = new List<CausesIf>();
            releasesIfRules = new List<ReleasesIf>();
            alwaysRules = new List<Always>();
            impossibleRules = new List<Impossible>();
            observableRules = new List<Observable>();

        }

        public void AddRule(IRule rule)
        {
            
        }
    }
}
