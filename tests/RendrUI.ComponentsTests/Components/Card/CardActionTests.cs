
using System.ComponentModel;
using RendrUI.Components.Card;
using Shouldly;

namespace RendrUI.ComponentsTests.Components;


public partial class CardTests : TestContextBase
{
    [Fact]
    public void CardAction_Renders_With_ChildContent()
    {
        var cut = Render<CardAction>(parameters => parameters
            .AddChildContent("Some content"));

        cut.Markup.ShouldContain("Some content");

        var card = cut.Find("div[data-slot='card-action']");
        card.ShouldNotBeNull();
    }

    [Fact]
    public void CardAction_Has_Default_Css_Class()
    {
        var cut = Render<CardAction>();

        var card = cut.Find("div[data-slot='card-action']");

        card.ClassList.ShouldContain("rui-card-action");
        card.ClassList.ShouldContain("col-start-2");
        card.ClassList.ShouldContain("row-span-2");
        card.ClassList.ShouldContain("row-start-1");
        card.ClassList.ShouldContain("self-start");
        card.ClassList.ShouldContain("justify-self-end");
    }

    [Fact]
    public void CardAction_Merges_Additional_Class_Attribute()
    {
        var cut = Render<CardAction>(parameters => parameters
            .AddUnmatched("class", "my-extra-class"));

        var card = cut.Find("div[data-slot='card-action']");

        card.ClassList.ShouldContain("rui-card-action");
        card.ClassList.ShouldContain("my-extra-class");
    }

    [Fact]
    public void CardAction_Renders_Arbitrary_Additional_Attributes()
    {
        var cut = Render<CardAction>(parameters => parameters
            .AddUnmatched("data-test-id", "test123")
            .AddUnmatched("aria-label", "card label"));

        var card = cut.Find("div[data-slot='card-action']");

        card.HasAttribute("data-test-id").ShouldBeTrue();
        card.GetAttribute("data-test-id").ShouldBe("test123");
        card.HasAttribute("aria-label").ShouldBeTrue();
        card.GetAttribute("aria-label").ShouldBe("card label");
    }

    [Fact]
    public void CardAction_Handles_Null_AdditionalAttributes()
    {
        var cut = Render<CardAction>(parameters => parameters
            .Add(p => p.AdditionalAttributes, null));

        var card = cut.Find("div[data-slot='card-action']");

        card.ClassList.ShouldContain("rui-card-action");
    }
}