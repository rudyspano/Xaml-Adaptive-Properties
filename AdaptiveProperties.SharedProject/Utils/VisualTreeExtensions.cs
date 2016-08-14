using System;
using System.Collections.Generic;
#if NETFX_CORE
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;
#else
using System.Windows;
using System.Windows.Media;
#endif

namespace AdaptiveProperties.Utils
{
    public static class VisualTreeExtensions
    {
        /// <summary>
        /// Get Parent control from VisualTree of a type compliant with condition
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="control"></param>
        /// <param name="conditonPredicate"></param>
        /// <returns></returns>
        public static T FindParentControl<T>(this DependencyObject control, Func<T, bool> conditonPredicate = null) where T : DependencyObject
        {
            var parent = VisualTreeHelper.GetParent(control);

            while (!(parent == null || (parent is T) && (conditonPredicate == null || conditonPredicate.Invoke((T)parent))))
            {
                parent = VisualTreeHelper.GetParent(parent);
            }

            return parent as T;
        }

        /// <summary>
        /// Get children control from VisualTree of a type compliant with condition
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="control"></param>
        /// <param name="conditonPredicate"></param>
        /// <returns></returns>
        public static T FindChildControl<T>(this DependencyObject control, Func<T, bool> conditonPredicate = null) where T : DependencyObject
        {
            int childNumber = VisualTreeHelper.GetChildrenCount(control);
            for (int i = 0; i < childNumber; i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(control, i);
                if (child != null && child is T && (conditonPredicate == null || conditonPredicate.Invoke((T)child)))
                    return (T)child;
                else
                    FindChildControl<T>(child);
            }
            return null;
        }

        public static List<T> FindChildrenControls<T>(this DependencyObject control) where T : DependencyObject
        {
            var childrens = new List<T>();

            int childNumber = VisualTreeHelper.GetChildrenCount(control);
            for (int i = 0; i < childNumber; i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(control, i);

                if (child == null)
                    continue;

                if (child is T)
                {
                    childrens.Add((T)child);
                }

                childrens.AddRange(FindChildrenControls<T>(child));
            }


            return childrens;
        }
    }
}
