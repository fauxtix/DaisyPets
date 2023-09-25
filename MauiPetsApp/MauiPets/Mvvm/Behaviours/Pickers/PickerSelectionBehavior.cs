using System.Windows.Input;

namespace MauiPets.Mvvm.Behaviours.Pickers;

public class PickerSelectionBehavior : Behavior<Picker>
{
    public static readonly BindableProperty CommandProperty =
        BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(PickerSelectionBehavior), default(ICommand));

    public ICommand Command
    {
        get => (ICommand)GetValue(CommandProperty);
        set => SetValue(CommandProperty, value);
    }

    protected override void OnAttachedTo(Picker bindable)
    {
        base.OnAttachedTo(bindable);
        bindable.SelectedIndexChanged += OnPickerSelectedIndexChanged;
    }

    protected override void OnDetachingFrom(Picker bindable)
    {
        base.OnDetachingFrom(bindable);
        bindable.SelectedIndexChanged -= OnPickerSelectedIndexChanged;
    }

    private void OnPickerSelectedIndexChanged(object sender, EventArgs e)
    {
        if (Command?.CanExecute(null) == true)
        {
            Command.Execute(null);
        }
    }
}