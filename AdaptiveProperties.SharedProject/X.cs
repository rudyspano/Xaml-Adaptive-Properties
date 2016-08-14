using System;
using System.Collections.Generic;
using System.Linq;

using AdaptiveProperties.Utils;
#if NETFX_CORE
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

using Windows.UI.Xaml.Media;
#else
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Xaml;
#endif
// ReSharper disable StaticMemberInGenericType

namespace AdaptiveProperties
{
    /// <summary>
    /// Attached properties and shared code
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class X<T> where T : PageModeClass, new()
    {

        #region Members

        internal static readonly List<WindowConfiguration> WindowConfigurations = new List<WindowConfiguration>();

        private static PageMode TransformationPageMode
        {
            get
            {
                var instance = new T();
                return ((PageModeClass)instance).PageMode;
            }
        }
        #endregion

        #region Attached Properties definitions
        //caution: use special default value in order to call property changed anyway


        public static readonly DependencyProperty AdaptiveGridProperty = DependencyProperty.RegisterAttached(
         "AdaptiveGrid", typeof(bool?), typeof(Grid), new PropertyMetadata(false, delegate (DependencyObject d, DependencyPropertyChangedEventArgs e)
         {
             AdaptiveModeChanged(d, e, RowDefinitionsProperty, ColumnDefinitionsProperty);
         }));

        public static readonly DependencyProperty RowDefinitionsProperty = DependencyProperty.RegisterAttached(
             "RowDefinitions", typeof(List<RowDefinition>), typeof(Grid), null);

        public static readonly DependencyProperty ColumnDefinitionsProperty = DependencyProperty.RegisterAttached(
            "ColumnDefinitions", typeof(List<ColumnDefinition>), typeof(Grid), null);

        public static readonly DependencyProperty ColumnSpanProperty = DependencyProperty.RegisterAttached(
            "ColumnSpan", typeof(int), typeof(X<T>), new PropertyMetadata(-1, CSChanged));

        public static readonly DependencyProperty RowSpanProperty = DependencyProperty.RegisterAttached(
            "RowSpan", typeof(int), typeof(X<T>), new PropertyMetadata(-1, RSChanged));

        public static readonly DependencyProperty ColumnProperty = DependencyProperty.RegisterAttached(
            "Column", typeof(int), typeof(X<T>), new PropertyMetadata(-1, CChanged));

        public static readonly DependencyProperty RowProperty = DependencyProperty.RegisterAttached(
            "Row", typeof(int), typeof(X<T>), new PropertyMetadata(-1, RChanged));

        public static readonly DependencyProperty MaxHeightProperty = DependencyProperty.RegisterAttached(
            "MaxHeight", typeof(double), typeof(X<T>), new PropertyMetadata(-1d, MHChanged));

        public static readonly DependencyProperty MaxWidthProperty = DependencyProperty.RegisterAttached(
            "MaxWidth", typeof(double), typeof(X<T>), new PropertyMetadata(-1d, MWChanged));


        public static readonly DependencyProperty HeightProperty = DependencyProperty.RegisterAttached(
            "Height", typeof(double), typeof(X<T>), new PropertyMetadata(-1d, HChanged));

        public static readonly DependencyProperty WidthProperty = DependencyProperty.RegisterAttached(
            "Width", typeof(double), typeof(X<T>), new PropertyMetadata(-1d, WCHanged));

        public static readonly DependencyProperty VisibilityProperty = DependencyProperty.RegisterAttached(
            "Visibility", typeof(Visibility?), typeof(X<T>), new PropertyMetadata(null, VChanged));

        public static readonly DependencyProperty MarginProperty = DependencyProperty.RegisterAttached(
            "Margin", typeof(Thickness?), typeof(X<T>), new PropertyMetadata(null, MChanged));

        public static readonly DependencyProperty VerticalAlignmentProperty = DependencyProperty.RegisterAttached(
         "VerticalAlignment", typeof(VerticalAlignment), typeof(X<T>), new PropertyMetadata(VerticalAlignment.Stretch, VAChanged));

        public static readonly DependencyProperty HorizontalAlignmentProperty = DependencyProperty.RegisterAttached(
       "HorizontalAlignment", typeof(HorizontalAlignment), typeof(X<T>), new PropertyMetadata(HorizontalAlignment.Stretch, HAChanged));

        public static readonly DependencyProperty SourceProperty = DependencyProperty.RegisterAttached(
     "Source", typeof(ImageSource), typeof(X<T>), new PropertyMetadata(null, SourceChanged));

        #endregion

        #region Handlers

        internal static void WCHanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {

            XSPropertyChanged(d, e, "Width", TransformationPageMode);
        }

        internal static void HChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            XSPropertyChanged(d, e, "Height", TransformationPageMode);
        }

        internal static void MHChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            XSPropertyChanged(d, e, "MaxHeight", TransformationPageMode);

        }

        internal static void MWChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            XSPropertyChanged(d, e, "MaxWidth", TransformationPageMode);
        }

        internal static void CSChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            XSPropertyChanged(d, e, "ColumnSpan", TransformationPageMode);
        }

        internal static void RSChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            XSPropertyChanged(d, e, "RowSpan", TransformationPageMode);
        }

        internal static void CChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            XSPropertyChanged(d, e, "Column", TransformationPageMode);
        }


        internal static void RChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            XSPropertyChanged(d, e, "Row", TransformationPageMode);
        }

        internal static void VChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            XSPropertyChanged(d, e, "Visibility", TransformationPageMode);
        }

        internal static void MChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            XSPropertyChanged(d, e, "Margin", TransformationPageMode);

        }

        internal static void VAChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            XSPropertyChanged(d, e, "VerticalAlignment", TransformationPageMode);

        }

        internal static void HAChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            XSPropertyChanged(d, e, "HorizontalAlignment", TransformationPageMode);

        }

        private static void SourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            XSPropertyChanged(d, e, "Source", TransformationPageMode);
        }


        internal static void XSPropertyChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e,
    string property, PageMode transformationPageMode)
        {
            var transformation = new Transformation(property, dependencyObject,
                GetInitialValue(dependencyObject, property), e.NewValue, transformationPageMode);

            if (!UpdateProperty(dependencyObject, transformation))
            {
                var frameworkElement = dependencyObject as FrameworkElement;

                if (frameworkElement != null)
                {
                    frameworkElement.Loaded += delegate
                    {
                        UpdateProperty(dependencyObject, transformation);
                    };
                }
            }

        }

        /// <summary>
        /// Update property if loaded or after when loaded
        /// </summary>
        /// <param name="d"></param>
        /// <param name="transformation"></param>
        private static bool UpdateProperty(DependencyObject d, Transformation transformation)
        {
            var currentWindow = GetCurrentWindow(d);

            if (currentWindow == null)
                return false;

            //not null when change after loading
            if (currentWindow == null)
                return false;

            var pageMode = GetCurrentPageMode(currentWindow);
            ApplyProperty(transformation, pageMode, currentWindow);

            return true;

        }

        internal static void AdaptiveModeChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e,
            DependencyProperty gridRowProperty, DependencyProperty gridColumnProperty)
        {
            var grid = dependencyObject as Grid;

            if (grid != null)
            {
                if (!UpdateAdaptivePageMode(grid, gridRowProperty, gridColumnProperty))
                {
                    grid.Loaded += delegate
                    {
                        UpdateAdaptivePageMode(grid, gridRowProperty, gridColumnProperty);
                    };
                }
            }

        }

        /// <summary>
        /// Impact properties depending on page mode
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="gridRowProperty"></param>
        /// <param name="gridColumnProperty"></param>
        /// <returns>True if refresh has been performed, false otherwise</returns>
        private static bool UpdateAdaptivePageMode(Grid grid, DependencyProperty gridRowProperty, DependencyProperty gridColumnProperty)
        {
#if NETFX_CORE
            //Avoid multiple adaptive grid
            var childAdaptiveGrid = VisualTreeExtensions.FindChildControl<Grid>(grid, XS.GetAdaptiveGrid) ??
                               VisualTreeExtensions.FindChildControl<Grid>(grid, XL.GetAdaptiveGrid);
#else
            var childAdaptiveGrid = VisualTreeExtensions.FindChildControl<Grid>(grid, XS.GetAdaptiveGrid);
#endif

            if (childAdaptiveGrid != null)
            {
                throw new InvalidOperationException("Cannot have multiple Adaptive properties in the same visual tree");
            }

            var currentWindow = GetCurrentWindow(grid);

            if (currentWindow == null)
                return false;


            //Create definition for parent grid
            CreateGridDefinitions(grid, gridRowProperty, gridColumnProperty, currentWindow);

            //for child grids
            foreach (var childGrid in VisualTreeExtensions.FindChildrenControls<Grid>(grid))
            {
                CreateGridDefinitions(childGrid, gridRowProperty, gridColumnProperty, currentWindow);
            }

            RefreshProperties(currentWindow);

            //refresh properties on size changed
            currentWindow.SizeChanged += delegate { RefreshProperties(currentWindow); };

            return true;
        }

        private static FrameworkElement GetCurrentWindow(DependencyObject grid)
        {
#if NETFX_CORE
            var currentWindow = VisualTreeExtensions.FindParentControl<Page>(grid);
#else
            var currentWindow = VisualTreeExtensions.FindParentControl<Window>(grid);
#endif

            return currentWindow;

        }

        /// <summary>
        /// Create grid Definitions
        /// for a given grid
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="gridRowProperty"></param>
        /// <param name="gridColumnProperty"></param>
        /// <param name="currentWindow"></param>
        private static void CreateGridDefinitions(Grid grid, DependencyProperty gridRowProperty,
            DependencyProperty gridColumnProperty, FrameworkElement currentWindow)
        {
            //Affect gridDefinitions
            var gridDefinitions = new GridDefinitions()
            {
                Grid = grid,
                RowDefinitions = (List<RowDefinition>)grid.GetValue(gridRowProperty) ?? null, //null=> Do not change definition
                ColumnDefinitions = (List<ColumnDefinition>)grid.GetValue(gridColumnProperty) ?? null
            };

            //Ensure passing by the transformation mechanism
            var property = "ResponsiveGrid";
            var transformation = new Transformation(property, grid,
                GetInitialValue(grid, property), gridDefinitions, TransformationPageMode);

            var currentPageMode = GetCurrentPageMode(currentWindow);
            ApplyProperty(transformation, currentPageMode, currentWindow);
        }

        private static PageMode GetCurrentPageMode(FrameworkElement currentWindow)
        {
            PageMode pageMode;

            if (TransformationPageMode == PageMode.XS)
            {
                pageMode = currentWindow.ActualWidth <= XS.MaxWidth
                    ? PageMode.XS
                    : PageMode.XL;
            }
            else
            {
                pageMode = currentWindow.ActualWidth >= XL.MinWidth
                    ? PageMode.XL
                    : PageMode.XS;
            }

            return pageMode;
        }

        /// <summary>
        /// Get existing windows configuration and create it if necessary
        /// </summary>
        /// <param name="currentWindow"></param>
        /// <returns></returns>
        private static WindowConfiguration GetWindowConfiguration(FrameworkElement currentWindow)
        {
            var windowConfiguration = WindowConfigurations.FirstOrDefault(conf => conf.Window == currentWindow);

            if (windowConfiguration == null)
            {
                windowConfiguration = new WindowConfiguration(currentWindow);
                WindowConfigurations.Add(windowConfiguration);

            }
            return windowConfiguration;
        }

        /// <summary>
        /// Re-affect properties if needed
        /// </summary>
        /// <param name="currentWindow"></param>
        private static void RefreshProperties(FrameworkElement currentWindow)
        {
            var newPageMode = GetCurrentPageMode(currentWindow);

            var windowConfiguration = GetWindowConfiguration(currentWindow);

            if (windowConfiguration != null && windowConfiguration.CurrentPageMode != newPageMode)
            {
                foreach (var xsProperty in windowConfiguration.Transformations)
                {
                    ApplyProperty(xsProperty, newPageMode, currentWindow);
                }

                windowConfiguration.CurrentPageMode = newPageMode;
            }
        }

        #endregion

        #region Helper Methods

        internal static object GetInitialValue(DependencyObject dependencyObject, string property)
        {

            var uiElement = dependencyObject as FrameworkElement;

            //Apply property
            if (uiElement != null)
            {
                switch (property)
                {
                    case "ColumnSpan":
                        return Grid.GetColumnSpan(uiElement);

                    case "RowSpan":
                        return Grid.GetRowSpan(uiElement);

                    case "Column":
                        return Grid.GetColumn(uiElement);

                    case "Row":
                        return Grid.GetRow(uiElement);

                    case "Height":
                        return uiElement.Height;

                    case "Width":
                        return uiElement.Width;

                    case "MaxHeight":
                        return uiElement.MaxHeight;

                    case "MaxWidth":
                        return uiElement.MaxWidth;

                    case "Visibility":
                        return uiElement.Visibility;

                    case "Margin":
                        return uiElement.Margin;

                    case "VerticalAlignment":
                        return uiElement.VerticalAlignment;

                    case "HorizontalAlignment":
                        return uiElement.HorizontalAlignment;

                    case "Source":
                        var image = uiElement as Image;

                        if (image == null)
                            throw new InvalidOperationException("Source can only be set on an Image Control");

                        return image.Source;

                    case "ResponsiveGrid":
                        var grid = uiElement as Grid;

                        if (grid != null)
                        {
                            return new GridDefinitions()
                            {
                                RowDefinitions = grid.RowDefinitions.ToList(),
                                ColumnDefinitions = grid.ColumnDefinitions.ToList()
                            };
                        }

                        break;

                }
            }

            return null;
        }

        internal static void ApplyProperty(Transformation transformation, PageMode pageMode, FrameworkElement currentWindow)
        {
            var windowConfiguration = GetWindowConfiguration(currentWindow);

            // Store properties once
            var existingProperty = windowConfiguration.Transformations.FirstOrDefault(xP => xP == transformation);

            if (existingProperty != null)
                transformation = existingProperty;
            else
            {
                windowConfiguration.Transformations.Add(transformation);
            }

            var applyTransformation = pageMode == transformation.TransformationPageMode;

            //optimization purpose: be sure same property is not applied twice (Loaded/resied events complexity)
            if (applyTransformation == transformation.CurrentlyApplied)
                return;

            var propertyName = transformation.Property;

            var uiElement = transformation.Object as FrameworkElement;

            //Apply property
            if (uiElement != null)
            {

                switch (propertyName)
                {
                    case "ColumnSpan":
                        Grid.SetColumnSpan(uiElement, applyTransformation ? (int)transformation.Value : (int)transformation.InitialValue);
                        break;

                    case "RowSpan":
                        Grid.SetRowSpan(uiElement, applyTransformation ? (int)transformation.Value : (int)transformation.InitialValue);
                        break;

                    case "Column":
                        Grid.SetColumn(uiElement, applyTransformation ? (int)transformation.Value : (int)transformation.InitialValue);
                        break;

                    case "Row":
                        Grid.SetRow(uiElement, applyTransformation ? (int)transformation.Value : (int)transformation.InitialValue);
                        break;

                    case "Height":
                        uiElement.Height = applyTransformation ? (double)transformation.Value : (double)transformation.InitialValue;
                        break;

                    case "Width":
                        uiElement.Width = applyTransformation ? (double)transformation.Value : (double)transformation.InitialValue;
                        break;

                    case "MaxHeight":
                        uiElement.MaxHeight = applyTransformation ? (double)transformation.Value : (double)transformation.InitialValue;
                        break;

                    case "MaxWidth":
                        uiElement.MaxWidth = applyTransformation ? (double)transformation.Value : (double)transformation.InitialValue;
                        break;

                    case "Visibility":
                        uiElement.Visibility = applyTransformation ? (Visibility)transformation.Value : (Visibility)transformation.InitialValue;
                        break;

                    case "Margin":
                        uiElement.Margin = applyTransformation ? (Thickness)transformation.Value : (Thickness)transformation.InitialValue;
                        break;

                    case "VerticalAlignment":
                        uiElement.VerticalAlignment = applyTransformation ? (VerticalAlignment)transformation.Value : (VerticalAlignment)transformation.InitialValue;
                        break;

                    case "HorizontalAlignment":
                        uiElement.HorizontalAlignment = applyTransformation ? (HorizontalAlignment)transformation.Value : (HorizontalAlignment)transformation.InitialValue;
                        break;

                    case "Source":
                        var image = uiElement as Image;

                        if (image == null)
                            throw new InvalidOperationException("Source can only be set on an Image Control");

                        image.Source = applyTransformation ? (ImageSource)transformation.Value : (ImageSource)transformation.InitialValue;

                        break;

                    case "ResponsiveGrid":
                        var grid = uiElement as Grid;

                        if (grid != null)
                        {
                            //update grid definitions
                            UpdateColumRowDefinitions(grid, transformation, applyTransformation);

                            ////update subgrid definitions
                            //var childGrids = grid.FindChildrenControls<Grid>();

                            //foreach (var childGrid in childGrids)
                            //{
                            //    UpdateColumRowDefinitions(childGrid, transformation, applyTransformation);
                            //}
                        }
                        break;
                }

                //optimization purpose
                transformation.CurrentlyApplied = applyTransformation;
            }
        }

        /// <summary>
        /// Update row and column definitions for a given grid
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="transformation"></param>
        /// <param name="applyTransformation">Apply value of transformation or restore initial</param>
        private static void UpdateColumRowDefinitions(Grid grid, Transformation transformation, bool applyTransformation)
        {
            GridDefinitions gridDefinitions;

            if (applyTransformation)
            {
                gridDefinitions = (GridDefinitions)transformation.Value;
            }
            else
            {
                gridDefinitions = (GridDefinitions)transformation.InitialValue;
            }

            if (gridDefinitions.RowDefinitions != null) //if no grid rowdefinitions, do not touch
            {
                grid.RowDefinitions.Clear();

                foreach (var rowDefinition in gridDefinitions.RowDefinitions)
                {
                    grid.RowDefinitions.Add(rowDefinition);
                }
            }

            if (gridDefinitions.ColumnDefinitions != null) //if no grid columndefinitions, do not touch
            {
                grid.ColumnDefinitions.Clear();

                foreach (var columnDefinition in gridDefinitions.ColumnDefinitions)
                {
                    grid.ColumnDefinitions.Add(columnDefinition);
                }
            }
        }

        #endregion

        #region SubClasses

        internal class WindowConfiguration
        {
            public FrameworkElement Window { get; }
            public List<Transformation> Transformations { get; }
            public PageMode? CurrentPageMode { get; set; }

            public WindowConfiguration(FrameworkElement window)
            {
                Window = window;
                Transformations = new List<Transformation>();
            }
        }

        internal class GridDefinitions
        {
            internal IEnumerable<RowDefinition> RowDefinitions { get; set; }
            internal IEnumerable<ColumnDefinition> ColumnDefinitions { get; set; }
            public Grid Grid { get; set; }
        }

        internal class Transformation
        {
            internal string Property { get; }
            internal DependencyObject Object { get; }
            internal object Value { get; }
            internal object InitialValue { get; }
            internal PageMode TransformationPageMode { get; }
            public bool CurrentlyApplied { get; set; }

            public Transformation(string property, DependencyObject o, object initialValue, object value, PageMode transformationPageMode)
            {
                Property = property;
                Object = o;
                InitialValue = initialValue;
                Value = value;
                TransformationPageMode = transformationPageMode;
            }

            public override bool Equals(object obj)
            {
                var xsProperty = obj as Transformation;

                if (xsProperty == null)
                    return false;

                return xsProperty.Property == this.Property
                  && xsProperty.Object == this.Object;
            }
        }

        #endregion
    }
}