using System;
using System.Collections.Generic;
using System.Text;

namespace DependencyInjectionContainer.Tests
{
    public class Child : IHuman
    {
        private INationality nationality;

        public Child(INationality nationality)
        {
            this.nationality = nationality;
        }

        public string Speak()
        {
            return "Child: " + nationality.SayHello();
        }
    }
}
