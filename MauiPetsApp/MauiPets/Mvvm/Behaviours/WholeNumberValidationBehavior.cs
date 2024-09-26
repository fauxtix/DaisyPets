namespace MauiPets.Mvvm.Behaviours
{
    public class WholeNumberValidationBehavior : Behavior<Entry>
    {
        protected override void OnAttachedTo(Entry bindable)
        {
            bindable.TextChanged += Bindable_TextChanged;
            base.OnAttachedTo(bindable);
        }

        protected override void OnDetachingFrom(Entry bindable)
        {
            bindable.TextChanged -= Bindable_TextChanged;
            base.OnDetachingFrom(bindable);
        }

        private void Bindable_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.NewTextValue))
            {
                bool isWholeNumber = int.TryParse(e.NewTextValue, out int value) && value > 0;
                if (!isWholeNumber)
                {
                    ((Entry)sender).Text = e.OldTextValue;
                }
            }
            else
            {
                ((Entry)sender).Text = e.OldTextValue;
            }
        }
    }
}
