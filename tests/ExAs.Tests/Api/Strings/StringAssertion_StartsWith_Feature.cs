﻿using NUnit.Framework;
using static ExAs.Utils.Creation.CreateNinjas;

namespace ExAs.Api.Strings
{
    [TestFixture]
    public class StringAssertion_StartsWith_Feature
    {
        [Test]
        public void ExpectingNar_OnNaruto_ShouldSucceed()
        {
            // act
            var result = Naruto().Evaluate(n => n.Member(x => x.Name).StartsWith("Nar"));

            // assert
            result.ExAssert(r => r.Member(x => x.succeeded)  .IsTrue()
                                  .Member(x => x.log)        .IsEqualTo("Ninja: ( )Name = 'Naruto'")
                                  .Member(x => x.expectation).IsEqualTo("(expected: starts with 'Nar')"));
        }

        [Test]
        public void ExpectingNar_OnTakahashi_ShouldFail()
        {
            // act
            var result = Kakashi().Evaluate(n => n.Member(x => x.Name).StartsWith("Nar"));
            
            // assert
            result.ExAssert(r => r.Member(x => x.succeeded).IsFalse()
                                  .Member(x => x.log)      .IsEqualTo("Ninja: (X)Name = 'Kakashi'"));
        }

        [Test]
        public void ExpectingKaka_ShouldLogKakaAsExpectation()
        {
            // act
            var result = Kakashi().Evaluate(n => n.Member(x => x.Name).StartsWith("Kaka"));

            // assert
            result.ExAssert(r => r.Member(x => x.expectation).IsEqualTo("(expected: starts with 'Kaka')"));
        }
    }
}