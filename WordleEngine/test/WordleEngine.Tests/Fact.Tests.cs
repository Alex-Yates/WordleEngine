using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace WordleEngine.Tests {
    public class Fact_Fact_Tests {

        /* I've tried managing the test data using both InlineData and  TestData Classes. This was nice because it 
         * provided a lovely, itemised set of tests in the Visual Studio test runner. However, the Visual Studio Test 
         * Discovery process seems to be borked:
         * https://xunit.net/faq/theory-data-stability-in-vs
         * 
         * Since the Test Runner was giving super flakey and unreliable results, I resorted to using TestData methods 
         * with the "DisableDiscoveryEnumeration" attribute (not supported for inline or classes). This forces Visual 
         * Studio to run all the tests. Unfortunately, they all get grouped together in the Visual Studio Test Runner, 
         * and they still don't *ALWAYS* run, but it's much more reliable than what I was doing before.
        */

        [Theory]
        [MemberData(nameof(FactsThatShouldWork), DisableDiscoveryEnumeration = true)]
        public void Fact_Fact_CanInitializeFactWithGoodParams(char letter, bool exists, int position, int total) {
            Fact f = new Fact(letter, exists, position, total);
            Assert.Equal(f.Letter, letter);
            Assert.Equal(f.Exists, exists);
            Assert.Equal(f.Position, position);
            Assert.Equal(f.Total, total);
        }

        [Theory]
        [MemberData(nameof(FactsThatShouldThrowExceptions), DisableDiscoveryEnumeration = true)]
        public void Fact_Fact_CannotInitializeFactWithBadParams(char letter, bool exists, int position, int total) {
            Assert.Throws<InvalidOperationException>(() => new Fact(letter, exists, position, total));
        }

        public static TheoryData<char, bool, int, int> FactsThatShouldWork {
            get {
                var data = new TheoryData<char, bool, int, int> {
                    { 'A', true, -1, -1 },  // Position and Total -1 should work
                    { 'A', true, 0, -1 },   // Position 0 should work
                    { 'A', true, 4, -1 },   // Position 4 should work
                    { 'A', false, -1, 0 },  // Total 0 with exists false should work
                    { 'A', true, -1, 4 }    // Total 4 should work
                };    
                return data;
            }
        }

        public static TheoryData<char, bool, int, int> FactsThatShouldThrowExceptions {
            get {
                var data = new TheoryData<char, bool, int, int> {
                    { 'A', true, -2, -1 },  // Position -2 should not be allowed
                    { 'A', true, 5, -1 },   // Position 5 should not be allowed
                    { 'A', true, -1, -2 },  // Total -2 should not be allowed
                    { 'A', true, -1, 6 },   // Total -6 should not be allowed
                    { 'A', true, -1, 0 },   // If exists, total cannot be 0
                    { 'A', false, -1, 1 },  // If not exists, total must be 0 (not 1)
                    { 'A', false, -1, -1 }  // If not exists, total must be 0 (not -1)
                };
                return data;
            }
        }
    }
}
