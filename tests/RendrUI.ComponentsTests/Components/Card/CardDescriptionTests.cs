
using System.ComponentModel;
using RendrUI.Components.Card;
using Shouldly;

namespace RendrUI.ComponentsTests.Components;


public partial class CardTests : TestContextBase
{
    [Fact]
    public void CardDescription_Renders_With_ChildContent()
    {
        var cut = Render<CardDescription>(parameters => parameters
            .AddChildContent("Some content"));

        cut.Markup.ShouldContain("Some content");

        var card = cut.Find("div[data-slot='card-description']");
        card.ShouldNotBeNull();
    }

    [Fact]
    public void CardDescription_Has_Default_Css_Class()
    {
        var cut = Render<CardDescription>();

        var card = cut.Find("div[data-slot='card-description']");

        card.ClassList.ShouldContain("rui-card-description");
    }

    [Fact]
    public void CardDescription_Merges_Additional_Class_Attribute()
    {
        var cut = Render<CardDescription>(parameters => parameters
            .AddUnmatched("class", "my-extra-class"));

        var card = cut.Find("div[data-slot='card-description']");

        card.ClassList.ShouldContain("rui-card-description");
        card.ClassList.ShouldContain("my-extra-class");
    }

    [Fact]
    public void CardDescription_Renders_Arbitrary_Additional_Attributes()
    {
        var cut = Render<CardDescription>(parameters => parameters
            .AddUnmatched("data-test-id", "test123")
            .AddUnmatched("aria-label", "card label"));

        var card = cut.Find("div[data-slot='card-description']");

        card.HasAttribute("data-test-id").ShouldBeTrue();
        card.GetAttribute("data-test-id").ShouldBe("test123");
        card.HasAttribute("aria-label").ShouldBeTrue();
        card.GetAttribute("aria-label").ShouldBe("card label");
    }

    [Fact]
    public void CardDescription_Handles_Null_AdditionalAttributes()
    {
        var cut = Render<CardDescription>(parameters => parameters
            .Add(p => p.AdditionalAttributes, null));

        var card = cut.Find("div[data-slot='card-description']");

        card.ClassList.ShouldContain("rui-card-description");
    }
}