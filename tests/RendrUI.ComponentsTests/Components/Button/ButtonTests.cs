using RendrUI.Components.Button;
using Shouldly;

namespace RendrUI.ComponentsTests.Components;

public class ButtonTests : TestContextBase
{
    [Fact]
    public void Button_Renders_With_ChildContent()
    {
        // Act
        var cut = Render<Button>(parameters => parameters
            .AddChildContent("Click Me"));

        // Assert
        cut.Markup.ShouldContain("Click Me");

        var button = cut.Find("button");
        button.ShouldNotBeNull();
    }

    [Fact]
    public void Button_DefaultVariant_Has_Correct_Classes()
    {
        var cut = Render<Button>(parameters => parameters
            .AddChildContent("button"));

        var button = cut.Find("button");
        var classAttr = button.GetAttribute("class");

        classAttr.ShouldNotBeNull();
        classAttr.ShouldContain("bg-primary");
        classAttr.ShouldContain("text-primary-foreground");
    }

    [Fact]
    public void Button_DestructiveVariant_Has_Red_Classes()
    {
        var cut = Render<Button>(parameters => parameters
            .Add(p => p.Variant, ButtonVariant.Destructive)
        );

        var classAttr = cut.Find("button").GetAttribute("class");

        classAttr.ShouldNotBeNull();
        classAttr.ShouldContain("bg-red-500");
        classAttr.ShouldContain("text-white");
    }

    [Theory]
    [InlineData(ButtonSize.Default, "h-10")]
    [InlineData(ButtonSize.Sm, "h-9")]
    [InlineData(ButtonSize.Lg, "h-11")]
    [InlineData(ButtonSize.Icon, "w-10")]
    public void Button_Size_Applies_Correct_Classes(ButtonSize size, string expectedClass)
    {
        var cut = Render<Button>(parameters => parameters
            .Add(p => p.Size, size));

        var classAttr = cut.Find("button").GetAttribute("class");

        classAttr.ShouldNotBeNull();
        classAttr.ShouldContain(expectedClass);
    }

    [Fact]
    public void Button_Disabled_Adds_Disabled_Attribute()
    {
        var cut = Render<Button>(parameters => parameters
            .AddUnmatched("disabled", true)
        );

        var button = cut.Find("button");

        button.HasAttribute("disabled").ShouldBeTrue();

        var classAttr = button.GetAttribute("class");

        classAttr.ShouldNotBeNull();
        classAttr.ShouldContain("disabled:pointer-events-none");
        classAttr.ShouldContain("disabled:opacity-50");
    }

    [Fact]
    public void Button_Type_Defaults_To_Button()
    {
        var cut = Render<Button>();

        cut.Find("button")
        .GetAttribute("type")
        .ShouldBe("button");
    }

    [Fact]
    public void Button_Type_Can_Be_Changed()
    {
        var cut = Render<Button>(parameters => parameters
            .AddUnmatched("type", "submit")
        );

        cut.Find("button")
        .GetAttribute("type")
        .ShouldBe("submit");
    }

    [Fact]
    public void Button_Merges_Custom_Class_From_AdditionalAttributes()
    {
        var cut = Render<Button>(parameters => parameters
            .AddUnmatched("class", "my-custom-class")
        );

        var classAttr = cut.Find("button").GetAttribute("class");

        classAttr.ShouldNotBeNull();
        classAttr.ShouldContain("my-custom-class");
        classAttr.ShouldContain("inline-flex");
    }

    [Fact]
    public void Button_Markup_Matches_Snapshot()
    {
        var cut = Render<Button>(parameters => parameters
            .Add(p => p.Variant, ButtonVariant.Secondary)
            .Add(p => p.Size, ButtonSize.Sm)
            .AddChildContent("Save")
        );

        cut.Markup.ShouldBe(
            "<button class=\"inline-flex items-center justify-center rounded-sm text-sm font-medium transition-colors focus-visible:outline-none focus-visible:ring-2 focus-visible:ring-ring focus-visible:ring-offset-2 disabled:pointer-events-none disabled:opacity-50 cursor-pointer bg-secondary text-secondary-foreground hover:bg-secondary/80 h-9 px-3\" type=\"button\">Save</button>"
        );
    }
}