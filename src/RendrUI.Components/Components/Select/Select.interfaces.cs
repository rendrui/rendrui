using System.Dynamic;

namespace RendrUI.Components.Select;


public interface ISelectContext
{
    bool IsOpen { get; }
    Task ToggleOpen();
    Task Close();
    Task SelectValue(object value);
    object? CurrentValue { get; }
    Type ValueType { get; }
    void SetSelectLabel(string label);
    string? SelectedLabel { get; set; }
}