using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Caliburn.Micro;

namespace CodeCamp.RIA.UI
{
    public static class Extensions
    {
        public static bool IsNullOrWhiteSpace(this string value)
        {
            return string.IsNullOrWhiteSpace(value);
        }

        public static string FormatWith(this string formatString, params object[] args)
        {
            return string.Format(formatString, args);
        }

        public static IResult ToSequential(this IEnumerable<IResult> results)
        {
            return new SequentialResult(results.GetEnumerator());
        }

        /// <summary>
        /// Find a value type object in a value-type collection.
        /// </summary>
        /// <typeparam name="T">Type of collection.</typeparam>
        /// <param name="collection">The collection to search.</param>
        /// <param name="isMatch">The Predicate to test to find the collection item.</param>
        /// <returns>The collection item.</returns>
        public static T GetValueItem<T>(this ICollection<T> collection, Predicate<T> isMatch)
        {
            foreach (var item in collection)
            {
                if (isMatch(item))
                    return item;
            }
            return default(T);
        }

        /// <summary>
        /// Find a reference type object in a collection of reference types.
        /// </summary>
        /// <typeparam name="T">Type of collection.</typeparam>
        /// <param name="collection">The collection to search.</param>
        /// <param name="isMatch">The Predicate to test to find the collection item.</param>
        /// <returns>The collection item.</returns>
        public static T GetReferenceItem<T>(this ICollection<T> collection, Predicate<T> isMatch)
                                            where T : class, new()
        {
            foreach (var item in collection)
            {
                if (isMatch(item))
                    return item;
            }
            return new T();
        }

        /// <summary>
        /// Get the next random item in an IEnumerable/>
        /// </summary>
        /// <typeparam name="T">The Type to be examined</typeparam>
        /// <param name="source">The source Enumerable</param>
        /// <returns>A single randomly picked item from the Enumerable</returns>
        public static T NextRandom<T>(this IEnumerable<T> source) 
                                      where T : class, new()
        {
            
            if (source != null && source.Count() > 0)
            {
                var gen = new Random((int) DateTime.UtcNow.Ticks);
                return source.Skip(gen.Next(0, source.Count() - 1) - 1).Take(1).FirstOrDefault();
            }
            return new T();

        }

        /// <summary>
        /// Finds the Visual Tree Descendants of a UIElement
        /// </summary>
        /// <remarks>
        /// Sample usage: if (textbox1.Descendents().OfType/<ScrollViewer/>().FirstOrDefault().ComputedVerticalScrollBarVisibility == Visibility.Visible)
        /// </remarks>
        /// <param name="root">The starting point to search, i.e., a TextBox</param>
        /// <returns>A collection of found descendants</returns>
        public static IEnumerable<DependencyObject> Descendents(this DependencyObject root)
        {
            int count = VisualTreeHelper.GetChildrenCount(root);
            for (int i = 0; i < count; i++)
            {
                var child = VisualTreeHelper.GetChild(root, i);
                yield return child;
                foreach (var descendent in Descendents(child))
                    yield return descendent;
            }
        }

        /// <summary>
        /// Converts an IEnmerable to an ObservableCollection inline
        /// </summary>
        /// <typeparam name="T">The type to be converted</typeparam>
        /// <param name="enumerableList">The list to convert</param>
        /// <returns>An ObservableCollection of the desired type</returns>
        public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> enumerableList)
        {
            if (enumerableList != null)
            {
                //create an emtpy observable collection object
                var observableCollection = new ObservableCollection<T>();

                //loop through all the records and add to observable collection object
                foreach (var item in enumerableList)
                    observableCollection.Add(item);

                //return the populated observable collection
                return observableCollection;
            }
            return null;
        }
    }
}
