using System;
using System.Collections.Generic;
using System.Text;

namespace DependencyInjectionContainer.Tests
{
    public class Boots<TProtection> : IFootwear<TProtection> where TProtection : IProtection
    {
        private string description;
        private IProtection protection;

        public Boots(TProtection protection)
        {
            description = "Boots: ";
            this.protection = protection;
        }

        public string GetDescription()
        {
            return description + protection.GetProtectionDescription();
        }
    }
}
