using Microsoft.AspNetCore.Components;
using RendrUI.Components.Select;
using Shouldly;

namespace RendrUI.ComponentsTests.Components;

public class SelectTests : TestContextBase
{

    [Fact]
    public void Select_Composition_Toggles_Content_With_Trigger_Click()
    {
        var value = string.Empty;

        var cut = Render<Select<string>>(parameters => parameters
            .Add(p => p.Value, value)
            .Add(p => p.ValueChanged, v => value = v)
            .AddChildContent(builder =>
            {
                builder.OpenComponent<SelectTrigger>(0);
                builder.CloseComponent();

                builder.OpenComponent<SelectContent>(1);
                builder.AddAttribute(2, "ChildContent", (RenderFragment)(contentBuilder =>
                {
                    contentBuilder.OpenComponent<SelectItem<string>>(3);
                    contentBuilder.AddAttribute(4, "Value", "Apple");
                    contentBuilder.AddAttribute(5, "ChildContent", (RenderFragment)(cc => cc.AddContent(6, "Apple")));
                    contentBuilder.CloseComponent();
                }));
                builder.CloseComponent();
            }));

        cut.Markup.ShouldNotContain("bg-popover");

        var button = cut.Find("button");
        button.Click();

        cut.Markup.ShouldContain("bg-popover");

        button = cut.Find("button");
        button.Click();

        cut.Markup.ShouldNotContain("bg-popover");
    }

    [Fact]
    public void Select_SelectItem_Updates_Value_Label_And_Closes_Content()
    {
        var selectedValue = string.Empty;

        var cut = Render<Select<string>>(parameters => parameters
            .Add(p => p.Value, selectedValue)
            .Add(p => p.ValueChanged, v => selectedValue = v)
            .AddChildContent(builder =>
            {
                builder.OpenComponent<SelectTrigger>(0);
                builder.CloseComponent();

                builder.OpenComponent<SelectContent>(1);
                builder.AddAttribute(2, "ChildContent", (RenderFragment)(contentBuilder =>
                {
                    contentBuilder.OpenComponent<SelectItem<string>>(3);
                    contentBuilder.AddAttribute(4, "Value", "Apple");
                    contentBuilder.AddAttribute(5, "ChildContent", (RenderFragment)(cc => cc.AddContent(6, "Apple")));
                    contentBuilder.CloseComponent();

                    contentBuilder.OpenComponent<SelectItem<string>>(7);
                    contentBuilder.AddAttribute(8, "Value", "Banana");
                    contentBuilder.AddAttribute(9, "ChildContent", (RenderFragment)(cc => cc.AddContent(10, "Banana")));
                    contentBuilder.CloseComponent();
                }));
                builder.CloseComponent();
            }));

        var trigger = cut.Find("button");
        trigger.TextContent.ShouldBe("Select an option");

        trigger.Click();

        var firstOption = cut.Find("div[role=option]");
        firstOption.Click();

        selectedValue.ShouldBe("Apple");

        trigger = cut.Find("button");
        trigger.TextContent.ShouldBe("Apple");

        cut.Markup.ShouldNotContain("bg-popover");
    }

    [Fact]
    public void SelectItem_Preselected_Value_Shows_Checkmark()
    {
        var value = "Banana";

        var cut = Render<Select<string>>(parameters => parameters
            .Add(p => p.Value, value)
            .Add(p => p.ValueChanged, v => value = v)
            .AddChildContent(builder =>
            {
                builder.OpenComponent<SelectTrigger>(0);
                builder.CloseComponent();

                builder.OpenComponent<SelectContent>(1);
                builder.AddAttribute(2, "ChildContent", (RenderFragment)(contentBuilder =>
                {
                    contentBuilder.OpenComponent<SelectItem<string>>(3);
                    contentBuilder.AddAttribute(4, "Value", "Apple");
                    contentBuilder.AddAttribute(5, "ChildContent", (RenderFragment)(cc => cc.AddContent(6, "Apple")));
                    contentBuilder.CloseComponent();

                    contentBuilder.OpenComponent<SelectItem<string>>(7);
                    contentBuilder.AddAttribute(8, "Value", "Banana");
                    contentBuilder.AddAttribute(9, "ChildContent", (RenderFragment)(cc => cc.AddContent(10, "Banana")));
                    contentBuilder.CloseComponent();
                }));
                builder.CloseComponent();
            }));

        var trigger = cut.Find("button");
        trigger.Click();

        var options = cut.FindAll("div[role=option]");
        options.Count.ShouldBe(2);

        options[0].InnerHtml.ShouldNotContain("✓");
        options[1].InnerHtml.ShouldContain("✓");
    }

    [Theory]
    [InlineData(SelectSize.Sm, "h-9")]
    [InlineData(SelectSize.Default, "h-10")]
    [InlineData(SelectSize.Lg, "h-11")]
    public void SelectTrigger_Size_Applies_Correct_Height_Class(SelectSize size, string expectedClass)
    {
        var value = string.Empty;

        var cut = Render<Select<string>>(parameters => parameters
            .Add(p => p.Value, value)
            .Add(p => p.ValueChanged, v => value = v)
            .AddChildContent(builder =>
            {
                builder.OpenComponent<SelectTrigger>(0);
                builder.AddAttribute(1, "Size", size);
                builder.CloseComponent();
            }));

        var button = cut.Find("button");
        var classAttr = button.GetAttribute("class");

        classAttr.ShouldNotBeNull();
        classAttr.ShouldContain(expectedClass);
    }

    [Fact]
    public void SelectTrigger_HasError_Uses_Error_Classes()
    {
        var value = string.Empty;

        var cut = Render<Select<string>>(parameters => parameters
            .Add(p => p.Value, value)
            .Add(p => p.ValueChanged, v => value = v)
            .AddChildContent(builder =>
            {
                builder.OpenComponent<SelectTrigger>(0);
                builder.AddAttribute(1, "HasError", true);
                builder.CloseComponent();
            }));

        var button = cut.Find("button");
        var classAttr = button.GetAttribute("class");

        classAttr.ShouldNotBeNull();
        classAttr.ShouldContain("border-destructive");
        classAttr.ShouldContain("focus-visible:ring-destructive");
        classAttr.ShouldNotContain("border-input");
    }

    [Fact]
    public void SelectTrigger_Merges_Custom_Class_From_AdditionalAttributes()
    {
        var value = string.Empty;

        var cut = Render<Select<string>>(parameters => parameters
            .Add(p => p.Value, value)
            .Add(p => p.ValueChanged, v => value = v)
            .AddChildContent(builder =>
            {
                builder.OpenComponent<SelectTrigger>(0);
                builder.AddAttribute(1, "class", "my-trigger");
                builder.CloseComponent();
            }));

        var button = cut.Find("button");
        var classAttr = button.GetAttribute("class");

        classAttr.ShouldNotBeNull();
        classAttr.ShouldContain("my-trigger");
        classAttr.ShouldContain("flex");
        classAttr.ShouldContain("w-full");
    }

}