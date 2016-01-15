﻿using ExAs.Utils;
using NUnit.Framework;
using static ExAs.Utils.Creation.CreateNinjas;

namespace ExAs.Api.Strings
{
    [TestFixture]
    public class StringAssertion_IsNotEmpty_Feature
    {
        [Test]
        public void OnNaruto_ShouldSucceed()
        {
            // act
            var result = Naruto().Evaluate(n => n.Property(x => x.Name).IsNotEmpty());

            // assert
            result.ExAssert(r => r.Fullfills(true, "Ninja: ( )Name = 'Naruto'", "(expected: not empty)"));
        }

        [Test]
        public void OnNamelessNinja_ShouldFail()
        {
            // act
            var result = NamelessNinja().Evaluate(n => n.Property(x => x.Name).IsNotEmpty());

            // assert
            result.ExAssert(r => r.Fullfills(false, "Ninja: (X)Name = ''", "(expected: not empty)"));
        }

        [Test]
        public void OnNullNinja_ShouldSucceed()
        {
            // act
            var result = NullNinja().Evaluate(n => n.Property(x => x.Name).IsNotEmpty());

            // assert
            result.ExAssert(r => r.Fullfills(true, "Ninja: ( )Name = null", "(expected: not empty)"));
        }
    }
}