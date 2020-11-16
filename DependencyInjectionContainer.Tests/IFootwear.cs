using System;
using System.Collections.Generic;
using System.Text;

namespace DependencyInjectionContainer.Tests
{
    public interface IFootwear<TProtection> where TProtection: IProtection
    {
        string GetDescription();
    }
}
