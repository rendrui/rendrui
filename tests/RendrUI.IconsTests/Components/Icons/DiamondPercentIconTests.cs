using RendrUI.Icons;
using RendrUI.IconsTests.Components.Base;
using Shouldly;

namespace RendrUI.IconsTests.Components;

public class DiamondPercentIconTests
    : IconContractTests<DiamondPercentIcon>
{
    protected override int ExpectedPathCount => 4;

    [Fact]
    public void Icon_Passes_Through_Arbitrary_Attributes()
    {
        var cut = Render<DiamondPercentIcon>(p =>
            p.AddUnmatched("aria-hidden", "true"));

        cut.Find("svg")
            .GetAttribute("aria-hidden")
            .ShouldBe("true");
    }
}