using System.Windows.Input;

namespace MauiPets.Mvvm.Behaviours.Pickers
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public class PickerSelectionBehavior : Behavior<Picker>
    {
        public static readonly BindableProperty CommandProperty =
            BindableProperty.CreateAttached(
                "Command",
                typeof(ICommand),
                typeof(PickerSelectionBehavior),
                null,
                propertyChanged: OnCommandChanged);

        public static readonly BindableProperty AfterSelectionActionProperty =
            BindableProperty.Create(nameof(AfterSelectionAction), typeof(Action<object>), typeof(PickerSelectionBehavior), null);

        public Action<object> AfterSelectionAction
        {
            get { return (Action<object>)GetValue(AfterSelectionActionProperty); }
            set { SetValue(AfterSelectionActionProperty, value); }
        }
        public static ICommand GetCommand(BindableObject view)
        {
            return (ICommand)view.GetValue(CommandProperty);
        }

        public static void SetCommand(BindableObject view, ICommand value)
        {
            view.SetValue(CommandProperty, value);
        }

        protected override void OnAttachedTo(Picker picker)
        {
            base.OnAttachedTo(picker);
            picker.SelectedIndexChanged += OnPickerSelectedIndexChanged;
        }

        protected override void OnDetachingFrom(Picker picker)
        {
            base.OnDetachingFrom(picker);
            picker.SelectedIndexChanged -= OnPickerSelectedIndexChanged;
        }

        private void Picker_SelectedIndexChanged(object sender, EventArgs e)
        {
            var picker = (Picker)sender;
            var selectedItem = picker.SelectedItem;

            if (GetCommand(picker) != null && GetCommand(picker).CanExecute(selectedItem))
            {
                GetCommand(picker).Execute(selectedItem);
            }
        }

        private void OnPickerSelectedIndexChanged(object sender, EventArgs e)
        {
            var picker = (Picker)sender;
            if (picker.SelectedIndex >= 0)
            {
                var selectedItem = picker.SelectedItem;
                AfterSelectionAction?.Invoke(selectedItem); // Execute the action after selection
            }
        }

        private static void OnCommandChanged(BindableObject bindable, object oldValue, object newValue)
        {
            // No need to attach/detach event handlers here, as they are handled in OnAttachedTo and OnDetachingFrom
        }
    }
}