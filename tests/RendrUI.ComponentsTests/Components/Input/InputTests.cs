using RendrUI.Components.Input;
using Shouldly;
using Microsoft.AspNetCore.Components;
using System.Runtime.CompilerServices;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Components.Forms;
using System.Linq.Expressions;
using System;

namespace RendrUI.ComponentsTests.Components;

public class InputTests : TestContextBase
{
    [Fact]
    public void Input_Will_Render_HTMLInputElement()
    {
        var value = "testValue";
        var cut = Render<Input>(parameters => parameters
            .Add(p => p.Value, value)
            .Add(p => p.ValueChanged, v => value = v)
            .Add(p => p.ValueExpression, () => value));

        var input = cut.Find("input");
        var valueAttr = input.GetAttribute("value");

        valueAttr.ShouldNotBeNull();
        valueAttr.ShouldBe("testValue");
    }


    [Fact]
    public void Input_ShouldApply_DefaultType()
    {
        var value = "testValue";
        var cut = Render<Input>(parameters => parameters
            .Add(p => p.Value, value)
            .Add(p => p.ValueChanged, v => value = v)
            .Add(p => p.ValueExpression, () => value));

        var input = cut.Find("input");
        var typeAttr = input.GetAttribute("type");

        typeAttr.ShouldNotBeNull();
        typeAttr.ShouldBe("text");
    }

    [Fact]
    public void Input_ShouldApply_DefaultSize()
    {
        var value = "testValue";
        var cut = Render<Input>(parameters => parameters
            .Add(p => p.Value, value)
            .Add(p => p.ValueChanged, v => value = v)
            .Add(p => p.ValueExpression, () => value));

        var input = cut.Find("input");
        var classAttr = input.GetAttribute("class");

        classAttr.ShouldNotBeNull();
        classAttr.ShouldContain("h-10");
        classAttr.ShouldContain("w-64");
    }


    [Fact]
    public void Input_ShouldRespect_TypeAttribute()
    {
        var value = "1";
        var cut = Render<Input>(parameters => parameters
            .AddUnmatched("type", "number")
            .Add(p => p.Value, value)
            .Add(p => p.ValueChanged, v => value = v)
            .Add(p => p.ValueExpression, () => value));

        var input = cut.Find("input");
        var typeAttr = input.GetAttribute("type");

        typeAttr.ShouldNotBeNull();
        typeAttr.ShouldBe("number");
    }


    [Theory]
    [InlineData(InputSize.Default, "h-10", "w-64")]
    [InlineData(InputSize.Sm, "h-9", "w-32")]
    [InlineData(InputSize.Lg, "h-11", "w-96")]
    public void Input_ShouldApply_CorrectClasses_ForSize(InputSize size, string expectedHeight, string expectedWidth)
    {
        var value = "1";
        var cut = Render<Input>(parameters => parameters
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


    private class TestModel
    {
        [Required]
#pragma warning disable CS8618 
        public string Name { get; set; }
#pragma warning restore CS8618 
    }


    [Fact]
    public void Input_Should_Show_Validation_Error_When_Empty()
    {
        var model = new TestModel { Name = "" };

        var cut = Render<EditForm>(parameters => parameters
            .Add(p => p.Model, model)
            .Add(p => p.ChildContent, (EditContext ctx) => builder =>
            {
                builder.OpenComponent<Input>(0);
                builder.AddAttribute(1, "Value", model.Name);
                builder.AddAttribute(2, "ValueChanged", EventCallback.Factory.Create<string>(this, v => model.Name = v));
                builder.AddAttribute(3, "ValueExpression", (Expression<Func<string>>)(() => model.Name));
                builder.CloseComponent();

                builder.OpenComponent<ValidationMessage<string>>(4);
                builder.AddAttribute(5, "For", (Expression<Func<string>>)(() => model.Name));
                builder.CloseComponent();

                builder.OpenComponent<DataAnnotationsValidator>(6);
                builder.CloseComponent();
            })
        );

        cut.Find("form").Submit();

        cut.Markup.ShouldContain("The Name field is required");
    }
}
