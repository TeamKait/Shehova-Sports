using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using static Sports.Classes.VisibilityController;

namespace Sports.Classes
{
    public static class VisibilityController
    {
        public const Visibility hidden = Visibility.Hidden;
        public const Visibility visible = Visibility.Visible;

        private static List<IButtonCondition> buttons = new List<IButtonCondition>();

        public static void SetButtonVisibility<T>(Button button, Func<bool> condition) where T : Page
        {
            buttons.Add(new ButtonCondition<T>(button, condition));
        }

        public static void CheckButtons()
        {
            foreach(var buttonCondition in buttons)
            {
                buttonCondition.CheckCondition();
            }
        }

        public class ButtonCondition<T> : IButtonCondition where T : Page
        {
            public Button button;
            public Type related_page;
            public Func<bool> condition;

            public ButtonCondition(Button button, Func<bool> condition)
            {
                this.button = button;
                this.related_page = typeof(T);
                this.condition = condition;
            }
            public void CheckCondition()
            {
                button.Visibility = condition() && related_page != Manager.currentPage.GetType() ? visible : hidden;
            }
        }

        public interface IButtonCondition
        {
            void CheckCondition();
        }
    }
}
