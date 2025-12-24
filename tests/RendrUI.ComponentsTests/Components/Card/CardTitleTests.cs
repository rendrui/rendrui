using System.ComponentModel;
using RendrUI.Components.Card;
using Shouldly;

namespace RendrUI.ComponentsTests.Components;


public partial class CardTests : TestContextBase
{
    [Fact]
    public void CardTitle_Renders_With_ChildContent()
    {
        var cut = Render<CardTitle>(parameters => parameters
            .AddChildContent("Some content"));

        cut.Markup.ShouldContain("Some content");

        var card = cut.Find("div[data-slot='card-title']");
        card.ShouldNotBeNull();
    }

    [Fact]
    public void CardTitle_Has_Default_Css_Class()
    {
        var cut = Render<CardTitle>();

        var card = cut.Find("div[data-slot='card-title']");

        card.ClassList.ShouldContain("rui-card-title");
        card.ClassList.ShouldContain("leading-none");
        card.ClassList.ShouldContain("font-semibold");
        card.ClassList.ShouldContain("text-lg");
    }

    [Fact]
    public void CardTitle_Merges_Additional_Class_Attribute()
    {
        var cut = Render<CardTitle>(parameters => parameters
            .AddUnmatched("class", "my-extra-class"));

        var card = cut.Find("div[data-slot='card-title']");

        card.ClassList.ShouldContain("rui-card-title");
        card.ClassList.ShouldContain("my-extra-class");
    }

    [Fact]
    public void CardTitle_Renders_Arbitrary_Additional_Attributes()
    {
        var cut = Render<CardTitle>(parameters => parameters
            .AddUnmatched("data-test-id", "test123")
            .AddUnmatched("aria-label", "card label"));

        var card = cut.Find("div[data-slot='card-title']");

        card.HasAttribute("data-test-id").ShouldBeTrue();
        card.GetAttribute("data-test-id").ShouldBe("test123");
        card.HasAttribute("aria-label").ShouldBeTrue();
        card.GetAttribute("aria-label").ShouldBe("card label");
    }

    [Fact]
    public void CardTitle_Handles_Null_AdditionalAttributes()
    {
        var cut = Render<CardTitle>(parameters => parameters
            .Add(p => p.AdditionalAttributes, null));

        var card = cut.Find("div[data-slot='card-title']");

        card.ClassList.ShouldContain("rui-card-title");
    }
}