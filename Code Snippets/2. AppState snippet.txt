using System;

namespace BlazorCommerce
{
    public class AppState
    {
        public bool DisplayCategoryMenuInHeaderNav { get; private set; }

        public event Action OnChange;

        public void SetDisplayCategoryMenuInHeaderNav(bool displayCategoryMenuInHeaderNav)
        {
            DisplayCategoryMenuInHeaderNav = displayCategoryMenuInHeaderNav;
            NotifyStateChanged();
        }

        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}