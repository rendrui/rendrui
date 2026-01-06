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
}

