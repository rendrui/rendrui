using Xunit;
using Shouldly;
using Microsoft.AspNetCore.Components;

public abstract class IconContractTests<TIcon> : IconTestContext
    where TIcon : IComponent
{
    protected abstract int ExpectedPathCount { get; }

    [Fact]
    public void Renders_Svg_Element()
    {
        var cut = Render<TIcon>();

        cut.Find("svg").ShouldNotBeNull();
    }

    [Fact]
    public void Applies_Default_Classes()
    {
        var cut = Render<TIcon>();

        var css = cut.Find("svg").GetAttribute("class");

        css.ShouldContain("w-6");
        css.ShouldContain("h-6");
    }

    [Fact]
    public void Applies_Stroke_And_StrokeWidth()
    {
        var cut = RenderComponent<TIcon>(p => p
            .Add(icon => (object)icon.Color, "red")
            .Add(icon => (object)icon.StrokeWidth, 4));

        var svg = cut.Find("svg");

        svg.GetAttribute("stroke").ShouldBe("red");
        svg.GetAttribute("stroke-width").ShouldBe("4");
    }

    [Fact]
    public void Has_Expected_Number_Of_Paths()
    {
        var cut = Render<TIcon>();

        cut.FindAll("path").Count.ShouldBe(ExpectedPathCount);
    }
}
