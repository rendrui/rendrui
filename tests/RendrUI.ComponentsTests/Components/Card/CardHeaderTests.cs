using System.ComponentModel;
using RendrUI.Components.Card;
using Shouldly;

namespace RendrUI.ComponentsTests.Components;


public partial class CardTests : TestContextBase
{
    [Fact]
    public void CardHeader_Renders_With_ChildContent()
    {
        var cut = Render<CardHeader>(parameters => parameters
            .AddChildContent("Some content"));

        cut.Markup.ShouldContain("Some content");

        var cardHeader = cut.Find("div[data-slot='card-header']");
        cardHeader.ShouldNotBeNull();
    }

    [Fact]
    public void CardHeader_Has_Default_Css_Class()
    {
        var cut = Render<CardHeader>();

        var cardHeader = cut.Find("div[data-slot='card-header']");

        cardHeader.ClassList.ShouldContain("rui-card-header");
        cardHeader.ClassList.ShouldContain("group/card-header");
        cardHeader.ClassList.ShouldContain("grid");
        cardHeader.ClassList.ShouldContain("auto-rows-min");
    }

    [Fact]
    public void CardHeader_Merges_Additional_Class_Attribute()
    {
        var cut = Render<CardHeader>(parameters => parameters
            .AddUnmatched("class", "my-extra-class"));

        var cardHeader = cut.Find("div[data-slot='card-header']");

        cardHeader.ClassList.ShouldContain("rui-card-header");
        cardHeader.ClassList.ShouldContain("my-extra-class");
    }

    [Fact]
    public void CardHeader_Renders_Arbitrary_Additional_Attributes()
    {
        var cut = Render<CardHeader>(parameters => parameters
            .AddUnmatched("data-test-id", "test123")
            .AddUnmatched("aria-label", "card label"));

        var cardHeader = cut.Find("div[data-slot='card-header']");

        cardHeader.HasAttribute("data-test-id").ShouldBeTrue();
        cardHeader.GetAttribute("data-test-id").ShouldBe("test123");
        cardHeader.HasAttribute("aria-label").ShouldBeTrue();
        cardHeader.GetAttribute("aria-label").ShouldBe("card label");
    }

    [Fact]
    public void CardHeader_Handles_Null_AdditionalAttributes()
    {
        var cut = Render<CardHeader>(parameters => parameters
            .Add(p => p.AdditionalAttributes, null));

        var cardHeader = cut.Find("div[data-slot='card-header']");

        cardHeader.ClassList.ShouldContain("rui-card-header");
    }

    [Fact]
    public void CardHeader_Applies_HasDataSlotCardAction_Classes_When_CardAction_Present()
    {
        var cut = Render<CardHeader>(parameters => parameters
            .AddChildContent("<div data-slot=\"card-action\">Action</div>"));

        var cardHeader = cut.Find("div[data-slot='card-header']");

        var classAttr = cardHeader.GetAttribute("class");

        classAttr.ShouldNotBeNull();
        classAttr.ShouldContain("has-data-[slot=card-action]:grid-cols-[1fr_auto]");
        classAttr.ShouldContain("has-data-[slot=card-action]:gap-x-4");
        classAttr.ShouldContain("has-data-[slot=card-description]:grid-rows-[auto_auto]");
        classAttr.ShouldContain("rui-card-header");
        classAttr.ShouldContain("group/card-header");
        classAttr.ShouldContain("grid");
        classAttr.ShouldContain("auto-rows-min");
    }
}