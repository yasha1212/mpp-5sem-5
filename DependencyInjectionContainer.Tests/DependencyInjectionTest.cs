using Microsoft.VisualStudio.TestTools.UnitTesting;
using DependencyInjectionContainer;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DependencyInjectionContainer.Tests
{
    [TestClass]
    public class DependencyInjectionTest
    {
        [TestMethod]
        public void Resolve_EnglishNatonality_HelloPhraseInEnglish()
        {
            var configuration = new DependenciesConfiguration();
            configuration.Register<INationality, EnglishNationality>();
            var provider = new DependencyProvider(configuration);

            var nationality = provider.Resolve<INationality>();

            Assert.AreEqual("Hello!", nationality.HelloPhrase);
        }

        [TestMethod]
        public void ResolveTransient_EnglishNationality_OnePhrase()
        {
            var configuration = new DependenciesConfiguration();
            configuration.Register<INationality, EnglishNationality>();
            var provider = new DependencyProvider(configuration);

            var nationality = provider.Resolve<INationality>();
            nationality.SayHello();
            nationality = provider.Resolve<INationality>();
            nationality.SayHello();

            Assert.AreEqual(1, nationality.PhraseCount);
        }

        [TestMethod]
        public void ResolveSingleton_EnglishNationality_TwoPhrases()
        {
            var configuration = new DependenciesConfiguration();
            configuration.Register<INationality, EnglishNationality>(Lifetime.Singleton);
            var provider = new DependencyProvider(configuration);

            var nationality = provider.Resolve<INationality>();
            nationality.SayHello();
            nationality = provider.Resolve<INationality>();
            nationality.SayHello();

            Assert.AreEqual(2, nationality.PhraseCount);
        }

        [TestMethod]
        public void Resolve_MultipleNationalities_TwoImplementations()
        {
            var configuration = new DependenciesConfiguration();
            configuration.Register<INationality, RussianNationality>();
            configuration.Register<INationality, EnglishNationality>();
            var provider = new DependencyProvider(configuration);

            var nationalities = provider.Resolve<IEnumerable<INationality>>();
            int implementationsCount = nationalities.Count();

            Assert.AreEqual(2, implementationsCount);
        }

        [TestMethod]
        public void ResolveWithRecursion_EnglishChild_ChildSaysHelloInEnglish()
        {
            var configuration = new DependenciesConfiguration();
            configuration.Register<INationality, EnglishNationality>();
            configuration.Register<IHuman, Child>();
            var provider = new DependencyProvider(configuration);

            var englishChild = provider.Resolve<IHuman>();
            var phrase = englishChild.Speak();

            Assert.AreEqual("Child: Hello!", phrase);
        }

        [TestMethod]
        public void ResolveGeneric_BootsWithWaterProtection_BootsDecriptionWithWaterProtection()
        {
            var configuration = new DependenciesConfiguration();
            configuration.Register<IProtection, WaterProtection>();
            configuration.Register<IFootwear<IProtection>, Boots<IProtection>>();
            var provider = new DependencyProvider(configuration);

            var boots = provider.Resolve<IFootwear<IProtection>>();
            var description = boots.GetDescription();

            Assert.AreEqual("Boots: Protected from water", description);
        }

        [TestMethod]
        public void ResolveOpenGeneric_BootsWithWaterProtection_BootsDescriptionWithWaterProtection()
        {
            var configuration = new DependenciesConfiguration();
            configuration.Register<IProtection, WaterProtection>();
            configuration.Register(typeof(IFootwear<>), typeof(Boots<>));
            var provider = new DependencyProvider(configuration);

            var boots = provider.Resolve<IFootwear<IProtection>>();
            var description = boots.GetDescription();

            Assert.AreEqual("Boots: Protected from water", description);
        }

        [TestMethod]
        public void ResolveNamedImplementation_ProtectionNamedImplementations_DescriptionOfDirtProtection()
        {
            var configuration = new DependenciesConfiguration();
            configuration.Register<IProtection, WaterProtection>(name: ImplementationName.First);
            configuration.Register<IProtection, DirtProtection>(name: ImplementationName.Second);
            var provider = new DependencyProvider(configuration);

            var protection = provider.Resolve<IProtection>(ImplementationName.Second);
            var description = protection.GetProtectionDescription();

            Assert.AreEqual("Protected from dirt", description);
        }

        [TestMethod]
        public void ResolveNamedImplementation_ShoesWithDependencyAttribute_ShoesDescriptionWithDirtProtection()
        {
            var configuration = new DependenciesConfiguration();
            configuration.Register<IProtection, WaterProtection>(name: ImplementationName.First);
            configuration.Register<IProtection, DirtProtection>(name: ImplementationName.Second);
            configuration.Register<IFootwear<IProtection>, Shoes<IProtection>>();
            var provider = new DependencyProvider(configuration);

            var shoes = provider.Resolve<IFootwear<IProtection>>();
            var description = shoes.GetDescription();

            Assert.AreEqual("Shoes: Protected from dirt", description);
        }
    }
}
