namespace DocoptNet.Tests
{
    using NUnit.Framework;
    using static DocoptNet.Tests.PatternFactory;

    [TestFixture]
    public class ListArgumentMatchTests
    {
        [Test]
        public void Should_match_required_two_args_into_a_list()
        {
            Assert.AreEqual(
                new MatchResult(true,
                                Leaves(),
                                Leaves(new Argument("N", new[] { "1", "2"}) )),
                new Required(new Argument("N"), new Argument("N")).Fix().Match(new Argument(null, "1"), new Argument(null, "2"))
                );
        }

        [Test]
        public void Should_match_oneormore_arg_into_a_list()
        {
            Assert.AreEqual(
                new MatchResult(true,
                                Leaves(),
                                Leaves(new Argument("N", new[] { "1", "2", "3" }) )),
                new OneOrMore(new Argument("N")).Fix().Match(new Argument(null, "1"), new Argument(null, "2"), new Argument(null, "3"))
                );
        }

        [Test]
        public void Should_match_required_and_oneormore_arg_into_a_list()
        {
            Assert.AreEqual(
                new MatchResult(true,
                                Leaves(),
                                Leaves(new Argument("N", new[] { "1", "2", "3" }) )),
                    new Required(new Argument("N"), new OneOrMore(new Argument("N"))).Fix().Match(new Argument(null, "1"), new Argument(null, "2"), new Argument(null, "3"))
                );
        }

        [Test]
        public void Should_match_doubly_required_arg_into_a_list()
        {
            Assert.AreEqual(
                new MatchResult(true,
                                Leaves(),
                                Leaves(new Argument("N", new[] { "1", "2" }) )),
                    new Required(new Argument("N"), new Required(new Argument("N"))).Fix().Match(new Argument(null, "1"), new Argument(null, "2"))
                );
        }
    }
}
