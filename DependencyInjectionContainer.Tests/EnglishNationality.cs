using System;
using System.Collections.Generic;
using System.Text;

namespace DependencyInjectionContainer.Tests
{
    public class EnglishNationality : INationality
    {
        public string HelloPhrase { get; }
        public int PhraseCount { get; private set; }

        public EnglishNationality()
        {
            HelloPhrase = "Hello!";
            PhraseCount = 0;
        }

        public string SayHello()
        {
            PhraseCount++;
            return HelloPhrase;
        }
    }
}
