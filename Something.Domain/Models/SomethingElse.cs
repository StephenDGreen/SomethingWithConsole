using System.Collections.Generic;

namespace Something.Domain.Models
{
    public class SomethingElse
    {
        private SomethingElse()
        {
        }

        private SomethingElse(string name)
        {
            this.Name = name;
            this.Somethings = new List<Something>();
        }

        public string Name { get; private set; }

        public List<Something> Somethings { get; private set; }
        public int Id { get; set; }
        public string Tag { get; set; }

        public static SomethingElse CreateNamedSomethingElse(string name)
        {
            if (name == null)
            {
                throw new System.ArgumentException("Parameter cannot be null", "name");
            }
            return new SomethingElse(name);
        }
    }
}
