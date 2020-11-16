using System;
using System.Collections.Generic;
using System.Text;

namespace DependencyInjectionContainer.Tests
{
    public class WaterProtection : IProtection
    {
        private string protectionDescription;

        public WaterProtection()
        {
            protectionDescription = "Protected from water";
        }

        public string GetProtectionDescription()
        {
            return protectionDescription;
        }
    }
}
