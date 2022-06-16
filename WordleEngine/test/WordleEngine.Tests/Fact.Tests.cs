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
                    { 'A', true, -1, -1 },  // A exists, but don't know position or total
                    { 'A', true, 0, -1 },   // A exists in postion 0, but there might be more
                    { 'A', true, 4, -1 },   // A exists in postion 4, but there might be more
                    { 'A', false, -1, 0 },  // A does not exists in the solution
                    { 'A', true, -1, 4 }    // There are 4 As in the solution, but do not know in what position

                };    
                return data;
            }
        }

        public static TheoryData<char, bool, int, int> FactsThatShouldThrowExceptions {
            get {
                var data = new TheoryData<char, bool, int, int> {
                    { 'A', true, -2, -1 },  // OUT OF BOUNDS: Position -2 should not be allowed
                    { 'A', true, 5, -1 },   // OUT OF BOUNDS: Position 5 should not be allowed
                    { 'A', true, -1, -2 },  // OUT OF BOUNDS: Total -2 should not be allowed
                    { 'A', true, -1, 6 },   // OUT OF BOUNDS: Total -6 should not be allowed
                    { 'A', false, -1, -1 }, // CONTRADICTION: A does not exists in any position AND I don't know how many A's exist
                    { 'A', true, -1, 0 },   // CONTRADICTION: A exists AND total number of As is 0
                    { 'A', false, -1, 1 },  // CONTRADICTION: A does not exists AND there is 1 A
                };
                return data;
            }
        }
    }
}
