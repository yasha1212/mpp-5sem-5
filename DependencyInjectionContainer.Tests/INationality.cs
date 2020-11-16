using System;
using System.Collections.Generic;
using System.Text;

namespace DependencyInjectionContainer.Tests
{
    public interface INationality
    {
        string HelloPhrase { get; }
        int PhraseCount { get; }

        string SayHello();
    }
}
