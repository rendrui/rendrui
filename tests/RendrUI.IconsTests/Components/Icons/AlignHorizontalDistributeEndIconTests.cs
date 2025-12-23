using RendrUI.Icons;
using RendrUI.IconsTests.Components.Base;
using Shouldly;

namespace RendrUI.IconsTests.Components;

public class AlignHorizontalDistributeEndIconTests
    : IconContractTests<AlignHorizontalDistributeEndIcon>
{
    protected override int ExpectedPathCount => 2;

    [Fact]
    public void Icon_Passes_Through_Arbitrary_Attributes()
    {
        var cut = Render<AlignHorizontalDistributeEndIcon>(p =>
            p.AddUnmatched("aria-hidden", "true"));

        cut.Find("svg")
            .GetAttribute("aria-hidden")
            .ShouldBe("true");
    }
}