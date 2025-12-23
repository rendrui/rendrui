using RendrUI.Icons;
using RendrUI.IconsTests.Components.Base;
using Shouldly;

namespace RendrUI.IconsTests.Components;

public class CroissantIconTests
    : IconContractTests<CroissantIcon>
{
    protected override int ExpectedPathCount => 5;

    [Fact]
    public void Icon_Passes_Through_Arbitrary_Attributes()
    {
        var cut = Render<CroissantIcon>(p =>
            p.AddUnmatched("aria-hidden", "true"));

        cut.Find("svg")
            .GetAttribute("aria-hidden")
            .ShouldBe("true");
    }
}