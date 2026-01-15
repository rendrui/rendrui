using System.Text.RegularExpressions;
using AngleSharp.Diffing.Extensions;
using RendrUI.Components.Switch;
using Shouldly;

namespace RendrUI.ComponentsTests.Components;

public class SwitchTests : TestContextBase
{
    [Fact]
    public void Switch_Will_Render_HTMLButtonElement()
    {
        var value = true;
        var cut = Render<Switch>(parameters => parameters
            .Add(p => p.Value, value)
            .Add(p => p.ValueChanged, v => value = v));

        var button = cut.Find("button");
        var valueAttr = button.GetAttribute("value");
        var span = cut.Find("span");

        button.ShouldNotBeNull()
            .GetAttribute("role")?
            .ShouldContain("switch");
        span.ShouldNotBeNull()
            .GetAttribute("class")?
            .ShouldContain("pointer-events-none");
    }

    [Fact]
    public void Switch_ShouldHave_ButtonType()
    {
        var value = true;
        var cut = Render<Switch>(parameters => parameters
            .Add(p => p.Value, value)
            .Add(p => p.ValueChanged, v => value = v));

        var button = cut.Find("button");

        button.ShouldNotBeNull();
        button.GetAttribute("type").ShouldBe("button");
    }

    [Fact]
    public void Switch_ShouldHave_SwitchRole()
    {
        var value = true;
        var cut = Render<Switch>(parameters => parameters
            .Add(p => p.Value, value)
            .Add(p => p.ValueChanged, v => value = v));

        var button = cut.Find("button");

        button.ShouldNotBeNull();
        button.GetAttribute("role").ShouldBe("switch");
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void Switch_ShouldPresent_CorrectDataSlotValue(bool value)
    {
        var cut = Render<Switch>(parameters => parameters
            .Add(p => p.Value, value)
            .Add(p => p.ValueChanged, v => value = v));

        var button = cut.Find("button");

        button.ShouldNotBeNull();
        button.GetAttribute("data-slot").ShouldBe(value ? "checked" : "unchecked");
    }

    [Fact]
    public void Switch_OnToggle_Should_ChangeValue()
    {
        var value = true;
        var cut = Render<Switch>(parameters => parameters
            .Add(p => p.Value, value)
            .Add(p => p.ValueChanged, v => value = v));

        var button = cut.Find("button");
        button.Click();
        button.GetAttribute("data-slot")?.ShouldBe("unchecked");
        value.ShouldBe(false);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void Swtich_ShouldRender_CorrectClasses_BasedOfState(bool value)
    {
        var cut = Render<Switch>(parameters => parameters
            .Add(p => p.Value, value)
            .Add(p => p.ValueChanged, v => value = v));

        var button = cut.Find("button");
        button.GetAttribute("class")?.ShouldContain(value ? "bg-primary" : "bg-input");
    }

    [Fact]
    public void Switch_ShouldAccept_ClassOverride()
    {
        var value = true;
        var customClass = "bg-red-500";
        var cut = Render<Switch>(parameters => parameters
            .Add(p => p.Value, value)
            .Add(p => p.ValueChanged, v => value = v)
            .AddUnmatched("class", customClass));

        var button = cut.Find("button");
        button.GetAttribute("class")?.ShouldContain("bg-red-500");
    }

    [Theory]
    [InlineData(SwitchSize.Sm, "h-4", "w-7", "h-3", "w-3")]
    [InlineData(SwitchSize.Default, "h-5", "w-9", "h-4", "w-4")]
    [InlineData(SwitchSize.Lg, "h-6", "w-11", "h-5", "w-5")]
    public void Switch_Size_ShouldRender_CorrectClasses(SwitchSize size, string bh, string bw, string sh, string sw)
    {
        var value = true;
        var cut = Render<Switch>(parameters => parameters
            .Add(p => p.Value, value)
            .Add(p => p.ValueChanged, v => value = v)
            .Add(p => p.Size, size));

        var button = cut.Find("button");
        button.GetAttribute("class")?.ShouldContain(bh);
        button.GetAttribute("class")?.ShouldContain(bw);

        var span = cut.Find("span");
        span.GetAttribute("class")?.ShouldContain(sh);
        span.GetAttribute("class")?.ShouldContain(sw);
    }
}