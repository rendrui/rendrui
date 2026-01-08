using RendrUI.Components.Checkbox;
using Shouldly;

namespace RendrUI.ComponentsTests.Components;


public class CheckboxTests : TestContextBase
{
    [Fact]
    public void Checkbox_Will_Render_HTMLInputElement()
    {
        var value = false;

        var cut = Render<Checkbox>(parameters => parameters
            .Add(p => p.Value, value)
            .Add(p => p.ValueChanged, v => value = v)
            .Add(p => p.ValueExpression, () => value));

        var input = cut.Find("input");

        input.ShouldNotBeNull();
        input.GetAttribute("type").ShouldBe("checkbox");
        input.HasAttribute("checked").ShouldBeFalse();
    }

    [Fact]
    public void Checkbox_ShouldApplexpectedHeighty_DefaultSize()
    {
        var value = false;

        var cut = Render<Checkbox>(parameters => parameters
            .Add(p => p.Value, value)
            .Add(p => p.ValueChanged, v => value = v)
            .Add(p => p.ValueExpression, () => value));

        var input = cut.Find("input");

        input.ShouldNotBeNull();
        input.GetAttribute("class")?.ShouldContain("h-4");
        input.GetAttribute("class")?.ShouldContain("w-4");
    }

    [Theory]
    [InlineData(CheckboxSize.Default, "h-4", "w-4")]
    [InlineData(CheckboxSize.Sm, "h-3", "w-3")]
    [InlineData(CheckboxSize.Lg, "h-6", "w-6")]
    public void Checkbox_ShouldApply_CorrectClasses_ForSize(CheckboxSize size, string expectedHeight, string expectedWidth)
    {
        var value = false;

        var cut = Render<Checkbox>(parameters => parameters
            .Add(p => p.Size, size)
            .Add(p => p.Value, value)
            .Add(p => p.ValueChanged, v => value = v)
            .Add(p => p.ValueExpression, () => value));

        var input = cut.Find("input");
        var classAttr = input.GetAttribute("class");

        classAttr.ShouldNotBeNull();
        classAttr.ShouldContain(expectedHeight);
        classAttr.ShouldContain(expectedWidth);
    }
}

