using Xunit;
using Shouldly;

namespace RendrUI.IconsTests.Components.Base;

public class IconBaseTests : IconTestContext
{
    [Fact]
    public void Adds_Default_Width_And_Height_When_Not_Provided()
    {
        var cut = Render<FakeIcon>();

        var css = cut.Find("svg").GetAttribute("class");

        css?.ShouldContain("w-6");
        css?.ShouldContain("h-6");
    }

    [Fact]
    public void Respects_User_Provided_Width()
    {
        var cut = Render<FakeIcon>(p =>
            p.AddUnmatched("class", "w-10"));

        var css = cut.Find("svg").GetAttribute("class");

        css?.ShouldContain("w-10");
        css?.ShouldNotContain("w-6");
    }

    [Fact]
    public void Respects_User_Provided_Height()
    {
        var cut = Render<FakeIcon>(p =>
            p.AddUnmatched("class", "h-12"));

        var css = cut.Find("svg").GetAttribute("class");

        css?.ShouldContain("h-12");
        css?.ShouldNotContain("h-6");
    }

    [Fact]
    public void Preserves_User_Classes()
    {
        var cut = Render<FakeIcon>(p =>
            p.AddUnmatched("class", "text-red-500"));

        var css = cut.Find("svg").GetAttribute("class");

        css?.ShouldContain("text-red-500");
    }

    [Fact]
    public void Passes_Through_Additional_Attributes()
    {
        var cut = Render<FakeIcon>(p =>
            p.AddUnmatched("data-test", "icon"));

        cut.Find("svg")
            .GetAttribute("data-test")
            .ShouldBe("icon");
    }
}
