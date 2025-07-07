namespace Loginapp.Services
{
    public class ToastService
    {
        // Define events using EventHandler or custom delegate
        public event EventHandler<(string Message, string Type)> OnShow;
        public event EventHandler OnHide;

        public void ShowToast(string message, string type = "info")
        {
            OnShow?.Invoke(this, (message, type));
        }

        public void HideToast()
        {
            OnHide?.Invoke(this, EventArgs.Empty);
        }
    }
}