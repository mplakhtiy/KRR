using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KRR.Logic.Rules
{
    class Always:IRule
    {
        public Fluent fluent { get; set; }
        public string Name { get; set; }
    }
}
