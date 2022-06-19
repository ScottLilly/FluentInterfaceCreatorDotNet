using System.Windows;

namespace FluentInterfaceCreator.WPF.Converters
{
    public class ConfigurableBooleanToVisibilityConverter : BooleanConverter<Visibility>
    {
        public ConfigurableBooleanToVisibilityConverter() 
            : base(Visibility.Visible, Visibility.Collapsed)
        {
        }
    }
}