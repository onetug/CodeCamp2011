using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using CodeCamp.RIA.Data.Web;

namespace CodeCamp.RIA.UI.Controls
{
    public partial class PreferenceValueRadioButton : UserControl
    {
        public PreferenceValueRadioButtonGroup ButtonGroup
        {
            get { return (PreferenceValueRadioButtonGroup)GetValue(ButtonGroupProperty); }
            set { SetValue(ButtonGroupProperty, value); }
        }

        public static readonly DependencyProperty ButtonGroupProperty =
            DependencyProperty.Register("ButtonGroupProperty", typeof(UserControl),
            typeof(PreferenceValueRadioButton), new PropertyMetadata(null, null));


        public PreferenceValue PreferenceValue
        {
            get { return (PreferenceValue)GetValue(PreferenceValueProperty); }
            set { SetValue(PreferenceValueProperty, value); }
        }

        public static readonly DependencyProperty PreferenceValueProperty =
            DependencyProperty.Register("PreferenceValueProperty", typeof(PreferenceValue),
            typeof(PreferenceValueRadioButton), new PropertyMetadata(null, null));


        public EventAttendee EventAttendee
        {
            get { return (EventAttendee)GetValue(EventAttendeeProperty); }
            set { SetValue(EventAttendeeProperty, value); }
        }
        public static readonly DependencyProperty EventAttendeeProperty =
            DependencyProperty.Register("EventAttendeeProperty", typeof(EventAttendee),
            typeof(PreferenceValueRadioButton), new PropertyMetadata(null, null));

        public EventAttendeePreferenceValue EventAttendeePreferenceValue
        {
            get { return (EventAttendeePreferenceValue)GetValue(EventAttendeePreferenceValueProperty); }
            set { SetValue(EventAttendeePreferenceValueProperty, value); }
        }

        public static readonly DependencyProperty EventAttendeePreferenceValueProperty =
            DependencyProperty.Register("EventAttendeePreferenceValueProperty", typeof(EventAttendeePreferenceValue),
            typeof(PreferenceValueRadioButton), new PropertyMetadata(null, null));

        public string Label
        {
            get { return (string)GetValue(LabelProperty); }
            set { SetValue(LabelProperty, value); }
        }

        public static readonly DependencyProperty LabelProperty =
            DependencyProperty.Register("LabelProperty", typeof(string),
            typeof(PreferenceValueRadioButton), new PropertyMetadata("Preference Value", null));

        //public bool IsChecked
        //{
        //    get { return (bool)GetValue(IsCheckedProperty); }
        //    set { SetValue(IsCheckedProperty, value); }
        //}

        //public static readonly DependencyProperty IsCheckedProperty =
        //    DependencyProperty.Register("IsCheckedProperty", typeof(bool),
        //    typeof(PreferenceValueRadioButton), new PropertyMetadata(false, null));

        public PreferenceValueRadioButton()
        {
            InitializeComponent();
            Loaded += ControlLoaded;
        }

        private void ControlLoaded(object sender, RoutedEventArgs e)
        {
            TheButton.Content = Label;
            Tag = PreferenceValue;
            TheButton.IsChecked = EventAttendeePreferenceValue != null ? EventAttendeePreferenceValue.PreferenceValues_Id == PreferenceValue.Id : false;
            TheButton.GroupName = ButtonGroup.Name;
        }
    }
}
