using MauiPets.Mvvm.Models;
using System.Collections;
using System.Windows.Input;

namespace MauiPets.Mvvm.Behaviours.Pets;
[XamlCompilation(XamlCompilationOptions.Compile)]
public class PickerSelectionChangedBehavior : Behavior<Picker>
{
    public static readonly BindableProperty CommandProperty =
        BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(PickerSelectionChangedBehavior));

    public static readonly BindableProperty IdProperty =
        BindableProperty.Create(nameof(Id), typeof(int), typeof(PickerSelectionChangedBehavior));

    public static readonly BindableProperty PropertyNameProperty =
        BindableProperty.Create(nameof(PropertyName), typeof(string), typeof(PickerSelectionChangedBehavior));

    public ICommand Command
    {
        get => (ICommand)GetValue(CommandProperty);
        set => SetValue(CommandProperty, value);
    }

    public int Id
    {
        get => (int)GetValue(IdProperty);
        set => SetValue(IdProperty, value);
    }

    public string PropertyName
    {
        get => (string)GetValue(PropertyNameProperty);
        set => SetValue(PropertyNameProperty, value);
    }

    protected override void OnAttachedTo(Picker picker)
    {
        base.OnAttachedTo(picker);
        picker.SelectedIndexChanged += Picker_SelectedIndexChanged;
    }

    protected override void OnDetachingFrom(Picker picker)
    {
        base.OnDetachingFrom(picker);
        picker.SelectedIndexChanged -= Picker_SelectedIndexChanged;
    }

    private void Picker_SelectedIndexChanged(object sender, EventArgs e)
    {
        var picker = (Picker)sender;
        var selectedItem = picker.SelectedItem;
        LookupTableVM selectedLookup = selectedItem as LookupTableVM;
        var _id = selectedLookup != null ? selectedLookup.Id : 0;

        if (Command != null && Command.CanExecute((Id, PropertyName)))
        {
            Command.Execute((Id, PropertyName));
        }
    }
}