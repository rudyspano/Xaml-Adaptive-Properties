using System.Collections.Generic;

#if NETFX_CORE
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
#else
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
#endif

namespace AdaptiveProperties
{
    /// <summary>
    /// XS Properties applied when 
    /// Width &lt;= XS.MaxWidth (600 by default) 
    /// </summary>
    public class XS : X<PageModeXSClass>
    {
        public static double MaxWidth = 600;
        #region Getters/Setters Required Here and not in Generic class in order to have xaml completion

        public static void SetAdaptiveGrid(DependencyObject element, bool value)
        {
            element.SetValue(AdaptiveGridProperty, value);
        }

        public static bool GetAdaptiveGrid(DependencyObject element)
        {
            return (bool)element.GetValue(AdaptiveGridProperty);
        }

        public static List<ColumnDefinition> GetColumnDefinitions(DependencyObject element)
        {
            var collection = (List<ColumnDefinition>)element.GetValue(ColumnDefinitionsProperty);

            if (collection == null)
            {
                collection = new List<ColumnDefinition>();
                element.SetValue(ColumnDefinitionsProperty, collection);
            }

            return collection;
        }


        public static void SetRowDefinitions(DependencyObject element, List<RowDefinition> value)
        {
            element.SetValue(RowDefinitionsProperty, value);
        }

        public static List<RowDefinition> GetRowDefinitions(DependencyObject element)
        {
            var collection = (List<RowDefinition>)element.GetValue(RowDefinitionsProperty);

            if (collection == null)
            {
                collection = new List<RowDefinition>();
                element.SetValue(RowDefinitionsProperty, collection);
            }

            return collection;
        }
        public static void SetColumnDefinitions(DependencyObject element, List<ColumnDefinition> value)
        {
            element.SetValue(ColumnDefinitionsProperty, value);
        }

        public static void SetRow(DependencyObject element, int value)
        {
            element.SetValue(RowProperty, value);
        }

        public static int GetRow(DependencyObject element)
        {
            return (int)element.GetValue(RowProperty);
        }

        public static void SetColumn(DependencyObject element, int value)
        {
            element.SetValue(ColumnProperty, value);
        }

        public static int GetColumn(DependencyObject element)
        {
            return (int)element.GetValue(ColumnProperty);
        }

        public static void SetRowSpan(DependencyObject element, int value)
        {
            element.SetValue(RowSpanProperty, value);
        }

        public static int GetRowSpan(DependencyObject element)
        {
            return (int)element.GetValue(RowSpanProperty);
        }

        public static void SetColumnSpan(DependencyObject element, int value)
        {
            element.SetValue(ColumnSpanProperty, value);
        }

        public static int GetColumnSpan(DependencyObject element)
        {
            return (int)element.GetValue(ColumnSpanProperty);
        }

        public static void SetWidth(DependencyObject element, double value)
        {
            element.SetValue(WidthProperty, value);
        }

        public static double GetWidth(DependencyObject element)
        {
            return (double)element.GetValue(WidthProperty);
        }

        public static void SetHeight(DependencyObject element, double value)
        {
            element.SetValue(HeightProperty, value);
        }

        public static double GetHeight(DependencyObject element)
        {
            return (double)element.GetValue(HeightProperty);
        }

        public static void SetMaxWidth(DependencyObject element, double value)
        {
            element.SetValue(MaxWidthProperty, value);
        }

        public static double GetMaxWidth(DependencyObject element)
        {
            return (double)element.GetValue(MaxWidthProperty);
        }

        public static void SetMaxHeight(DependencyObject element, double value)
        {
            element.SetValue(MaxHeightProperty, value);
        }

        public static double GetMaxHeight(DependencyObject element)
        {
            return (double)element.GetValue(MaxHeightProperty);
        }

        public static void SetVisibility(DependencyObject element, Visibility value)
        {
            element.SetValue(VisibilityProperty, value);
        }

        public static Visibility GetVisibility(DependencyObject element)
        {
            return (Visibility)element.GetValue(VisibilityProperty);
        }

        public static void SetMargin(DependencyObject element, Thickness value)
        {
            element.SetValue(MarginProperty, value);
        }

        public static Thickness GetMargin(DependencyObject element)
        {
            return (Thickness)element.GetValue(MarginProperty);
        }

        public static void SetVerticalAlignment(DependencyObject element, VerticalAlignment value)
        {
            element.SetValue(VerticalAlignmentProperty, value);
        }

        public static VerticalAlignment GetVerticalAlignment(DependencyObject element)
        {
            return (VerticalAlignment)element.GetValue(VerticalAlignmentProperty);
        }

        public static void SetHorizontalAlignment(DependencyObject element, HorizontalAlignment value)
        {
            element.SetValue(HorizontalAlignmentProperty, value);
        }

        public static HorizontalAlignment GetHorizontalAlignment(DependencyObject element)
        {
            return (HorizontalAlignment)element.GetValue(HorizontalAlignmentProperty);
        }

        public static void SetSource(DependencyObject element, ImageSource value)
        {
            element.SetValue(SourceProperty, value);
        }

        public static ImageSource GetSource(DependencyObject element)
        {
            return (ImageSource)element.GetValue(SourceProperty);
        }

        #endregion

    }
}