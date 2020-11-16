using System;
using System.Collections.Generic;
using System.Text;

namespace DependencyInjectionContainer.Tests
{
    public class RussianNationality : INationality
    {
        public string HelloPhrase { get; }

        public int PhraseCount { get; private set; }

        public RussianNationality()
        {
            HelloPhrase = "Привет!";
            PhraseCount = 0;
        }

        public string SayHello()
        {
            PhraseCount++;
            return HelloPhrase;
        }
    }
}
