using System;
using System.Collections.Generic;
using System.Text;

namespace DependencyInjectionContainer.Tests
{
    public class DirtProtection : IProtection
    {
        private string protectionDescription;

        public DirtProtection()
        {
            protectionDescription = "Protected from dirt";
        }

        public string GetProtectionDescription()
        {
            return protectionDescription;
        }
    }
}
