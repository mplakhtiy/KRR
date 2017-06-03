namespace KRR.Logic
{
    public class Fluent
    {
        public string Name { get; set; }
        public bool IsTrue { get; set; }

        public Fluent(string name, bool isTrue)
        {
            this.Name = name;
            this.IsTrue = isTrue;
        }

        public Fluent(Fluent fluent)
        {
            this.Name = fluent.Name;
            this.IsTrue = fluent.IsTrue;
        }
        public string toString()
        {
            return Name;
        }


    }
}
