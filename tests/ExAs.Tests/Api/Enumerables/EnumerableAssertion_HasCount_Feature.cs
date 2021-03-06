﻿using ExAs.Utils;
using NUnit.Framework;
using static ExAs.Utils.Creation.CreateCities;

namespace ExAs.Api.Enumerables
{
    [TestFixture]
    public class EnumerableAssertion_HasCount_Feature
    {
        [Test]
        public void Expecting1_OnCityWithDojo_ShouldSucceed()
        {
            // act
            var result = CityWithDojo().Evaluate(c => c.Member(x => x.Dojos).HasCount(1));

            // assert
            result.ExAssert(r => r.IsNotNull()
                                  .p(x => x.succeeded).IsTrue()
                                  .p(x => x.PrintLog()).IsEqualTo("City: ( )Dojos = <1 Dojo> (expected: 1 Dojo)"));
        }

        [Test]
        public void Expecting2_OnCityWithDojo_ShouldFail()
        {
            // act
            var result = CityWithDojo().Evaluate(c => c.p(x => x.Dojos).HasCount(2));

            // assert
            result.ExAssert(r => r.IsNotNull()
                                  .p(x => x.succeeded).IsFalse()
                                  .p(x => x.PrintLog()).IsEqualTo("City: (X)Dojos = <1 Dojo> (expected: 2 Dojos)"));
        }

        [Test]
        public void Expecting1_OnThreeDojoCity_ShouldFail()
        {
            // act
            var result = ThreeDojoCity().Evaluate(c => c.p(x => x.Dojos).HasCount(1));

            // assert
            result.ExAssert(r => r.IsNotNull()
                                  .p(x => x.succeeded).IsFalse()
                                  .p(x => x.PrintLog()).IsEqualTo("City: (X)Dojos = <3 Dojos> (expected: 1 Dojo)"));
        }
    }
}