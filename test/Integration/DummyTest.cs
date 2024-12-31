using FluentAssertions;

namespace VsExample.Tests.Integration;

public class DummyTest
{
    [Fact]
    public void Test1()
    {
        true.Should().BeTrue();
    }
}