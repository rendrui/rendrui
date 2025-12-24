using System.ComponentModel;
using RendrUI.Components.Card;
using Shouldly;

namespace RendrUI.ComponentsTests.Components;


public partial class CardTests : TestContextBase
{
    [Fact]
    public void CardFooter_Renders_With_ChildContent()
    {
        var cut = Render<CardFooter>(parameters => parameters
            .AddChildContent("Some content"));

        cut.Markup.ShouldContain("Some content");

        var card = cut.Find("div[data-slot='card-footer']");
        card.ShouldNotBeNull();
    }

    [Fact]
    public void CardFooter_Has_Default_Css_Class()
    {
        var cut = Render<CardFooter>();

        var card = cut.Find("div[data-slot='card-footer']");

        card.ClassList.ShouldContain("rui-card-footer");
        card.ClassList.ShouldContain("flex");
        card.ClassList.ShouldContain("items-center");
        card.ClassList.ShouldContain("flex-col");
        card.ClassList.ShouldContain("px-6");
        card.ClassList.ShouldContain("gap-2");
    }

    [Fact]
    public void CardFooter_Merges_Additional_Class_Attribute()
    {
        var cut = Render<CardFooter>(parameters => parameters
            .AddUnmatched("class", "my-extra-class"));

        var card = cut.Find("div[data-slot='card-footer']");

        card.ClassList.ShouldContain("rui-card-footer");
        card.ClassList.ShouldContain("my-extra-class");
    }

    [Fact]
    public void CardFooter_Renders_Arbitrary_Additional_Attributes()
    {
        var cut = Render<CardFooter>(parameters => parameters
            .AddUnmatched("data-test-id", "test123")
            .AddUnmatched("aria-label", "card label"));

        var card = cut.Find("div[data-slot='card-footer']");

        card.HasAttribute("data-test-id").ShouldBeTrue();
        card.GetAttribute("data-test-id").ShouldBe("test123");
        card.HasAttribute("aria-label").ShouldBeTrue();
        card.GetAttribute("aria-label").ShouldBe("card label");
    }

    [Fact]
    public void CardFooter_Handles_Null_AdditionalAttributes()
    {
        var cut = Render<CardFooter>(parameters => parameters
            .Add(p => p.AdditionalAttributes, null));

        var card = cut.Find("div[data-slot='card-footer']");

        card.ClassList.ShouldContain("rui-card-footer");
    }
}