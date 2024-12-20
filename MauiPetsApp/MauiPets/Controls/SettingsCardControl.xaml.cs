using System.Windows.Input;

namespace MauiPets.Controls
{
    public partial class SettingsCardControl : ContentView
    {
        public SettingsCardControl()
        {
            InitializeComponent();
            UpdateCommandParameter();
        }

        public static readonly BindableProperty LabelTextProperty = BindableProperty.Create(
            nameof(LabelText),
            typeof(string),
            typeof(SettingsCardControl),
            default(string));

        public string LabelText
        {
            get => (string)GetValue(LabelTextProperty);
            set => SetValue(LabelTextProperty, value);
        }

        public static readonly BindableProperty TableNameProperty = BindableProperty.Create(
            nameof(TableName),
            typeof(string),
            typeof(SettingsCardControl),
            default(string),
            propertyChanged: OnTableNameOrTitleChanged); // Notify on change

        public string TableName
        {
            get => (string)GetValue(TableNameProperty);
            set => SetValue(TableNameProperty, value);
        }

        public static readonly BindableProperty TitleProperty = BindableProperty.Create(
            nameof(Title),
            typeof(string),
            typeof(SettingsCardControl),
            default(string),
            propertyChanged: OnTableNameOrTitleChanged); // Notify on change

        public string Title
        {
            get => (string)GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }

        public static readonly BindableProperty IconProperty = BindableProperty.Create(
            nameof(Icon),
            typeof(string),
            typeof(SettingsCardControl), string.Empty);

        public string Icon
        {
            get => (string)GetValue(IconProperty);
            set => SetValue(IconProperty, value);
        }

        public static readonly BindableProperty ButtonCommandProperty = BindableProperty.Create(
            nameof(ButtonCommand),
            typeof(ICommand),
            typeof(SettingsCardControl),
            default(ICommand));

        public ICommand ButtonCommand
        {
            get => (ICommand)GetValue(ButtonCommandProperty);
            set => SetValue(ButtonCommandProperty, value);
        }

        public static readonly BindableProperty CommandParameterProperty = BindableProperty.Create(
            nameof(CommandParameter),
            typeof(object),
            typeof(SettingsCardControl),
            default(object));

        public object CommandParameter
        {
            get => GetValue(CommandParameterProperty);
            set => SetValue(CommandParameterProperty, value);
        }

        private void UpdateCommandParameter()
        {
            CommandParameter = new Dictionary<string, string>
            {
                { "TableName", TableName },
                { "Title", Title  }
            };
        }

        private static void OnTableNameOrTitleChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (SettingsCardControl)bindable;
            control.UpdateCommandParameter(); // Update CommandParameter on property change
        }

        private void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
        {
        }
    }
}
