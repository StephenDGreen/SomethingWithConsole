using Something.Domain.Models;
using System;

namespace Something.Domain
{
    public class SomethingElseFactory : ISomethingElseFactory
    {
        public SomethingElse Create(string name)
        {
            return SomethingElse.CreateNamedSomethingElse(name);
        }
    }
}