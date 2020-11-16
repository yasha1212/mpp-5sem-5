using System;
using System.Collections.Generic;
using System.Text;

namespace DependencyInjectionContainer.Tests
{
    public class Shoes<TProtection> : IFootwear<TProtection> where TProtection : IProtection
    {
        private string description;
        private IProtection protection;

        public Shoes([DependencyKey(ImplementationName.Second)]TProtection protection)
        {
            description = "Shoes: ";
            this.protection = protection;
        }

        public string GetDescription()
        {
            return description + protection.GetProtectionDescription();
        }
    }
}
