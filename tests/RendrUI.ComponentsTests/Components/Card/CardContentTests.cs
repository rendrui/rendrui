
using System.ComponentModel;
using RendrUI.Components.Card;
using Shouldly;

namespace RendrUI.ComponentsTests.Components;


public partial class CardTests : TestContextBase
{
    [Fact]
    public void CardContent_Renders_With_ChildContent()
    {
        var cut = Render<CardContent>(parameters => parameters
            .AddChildContent("Some content"));

        cut.Markup.ShouldContain("Some content");

        var card = cut.Find("div[data-slot='card-content']");
        card.ShouldNotBeNull();
    }

    [Fact]
    public void CardContent_Has_Default_Css_Class()
    {
        var cut = Render<CardContent>();

        var card = cut.Find("div[data-slot='card-content']");

        card.ClassList.ShouldContain("rui-card-content");
        card.ClassList.ShouldContain("px-6");
        card.ClassList.ShouldContain("pb-6");
    }

    [Fact]
    public void CardContent_Merges_Additional_Class_Attribute()
    {
        var cut = Render<CardContent>(parameters => parameters
            .AddUnmatched("class", "my-extra-class"));

        var card = cut.Find("div[data-slot='card-content']");

        card.ClassList.ShouldContain("rui-card-content");
        card.ClassList.ShouldContain("my-extra-class");
    }

    [Fact]
    public void CardContent_Renders_Arbitrary_Additional_Attributes()
    {
        var cut = Render<CardContent>(parameters => parameters
            .AddUnmatched("data-test-id", "test123")
            .AddUnmatched("aria-label", "card label"));

        var card = cut.Find("div[data-slot='card-content']");

        card.HasAttribute("data-test-id").ShouldBeTrue();
        card.GetAttribute("data-test-id").ShouldBe("test123");
        card.HasAttribute("aria-label").ShouldBeTrue();
        card.GetAttribute("aria-label").ShouldBe("card label");
    }

    [Fact]
    public void CardContent_Handles_Null_AdditionalAttributes()
    {
        var cut = Render<CardContent>(parameters => parameters
            .Add(p => p.AdditionalAttributes, null));

        var card = cut.Find("div[data-slot='card-content']");

        card.ClassList.ShouldContain("rui-card-content");
    }
}