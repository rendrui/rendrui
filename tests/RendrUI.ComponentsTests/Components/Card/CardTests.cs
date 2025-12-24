using System.ComponentModel;
using RendrUI.Components.Card;
using Shouldly;

namespace RendrUI.ComponentsTests.Components;


public partial class CardTests : TestContextBase
{
    [Fact]
    public void Card_Renders_With_ChildContent()
    {
        var cut = Render<Card>(parameters => parameters
            .AddChildContent("Some content"));

        cut.Markup.ShouldContain("Some content");

        var card = cut.Find("div[data-slot='card']");
        card.ShouldNotBeNull();
    }

    [Fact]
    public void Card_Has_Default_Css_Class()
    {
        var cut = Render<Card>();

        var card = cut.Find("div[data-slot='card']");

        card.ClassList.ShouldContain("rui-card");
        card.ClassList.ShouldContain("flex");
        card.ClassList.ShouldContain("bg-card");
    }

    [Fact]
    public void Card_Merges_Additional_Class_Attribute()
    {
        var cut = Render<Card>(parameters => parameters
            .AddUnmatched("class", "my-extra-class"));

        var card = cut.Find("div[data-slot='card']");

        card.ClassList.ShouldContain("rui-card");
        card.ClassList.ShouldContain("my-extra-class");
    }

    [Fact]
    public void Card_Renders_Arbitrary_Additional_Attributes()
    {
        var cut = Render<Card>(parameters => parameters
            .AddUnmatched("data-test-id", "test123")
            .AddUnmatched("aria-label", "card label"));

        var card = cut.Find("div[data-slot='card']");

        card.HasAttribute("data-test-id").ShouldBeTrue();
        card.GetAttribute("data-test-id").ShouldBe("test123");
        card.HasAttribute("aria-label").ShouldBeTrue();
        card.GetAttribute("aria-label").ShouldBe("card label");
    }

    [Fact]
    public void Card_Handles_Null_AdditionalAttributes()
    {
        var cut = Render<Card>(parameters => parameters
            .Add(p => p.AdditionalAttributes, null));

        var card = cut.Find("div[data-slot='card']");

        card.ClassList.ShouldContain("rui-card");
    }
}