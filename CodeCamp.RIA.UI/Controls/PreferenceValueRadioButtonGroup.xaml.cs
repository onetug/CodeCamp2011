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
    public partial class PreferenceValueRadioButtonGroup : UserControl
    {

        public Preference Preference
        {
            get { return (Preference)GetValue(PreferenceProperty); }
            set { SetValue(PreferenceProperty, value); }
        }

        public static readonly DependencyProperty PreferenceProperty =
            DependencyProperty.Register("PreferenceProperty", typeof(Preference),
            typeof(PreferenceValueRadioButtonGroup), new PropertyMetadata(null, null));

        public EventAttendee EventAttendee
        {
            get { return (EventAttendee)GetValue(EventAttendeeProperty); }
            set { SetValue(EventAttendeeProperty, value); }
        }
        public static readonly DependencyProperty EventAttendeeProperty =
            DependencyProperty.Register("EventAttendeeProperty", typeof(EventAttendee),
            typeof(PreferenceValueRadioButtonGroup), new PropertyMetadata(null, null));

        public string Label
        {
            get { return (string)GetValue(LabelProperty); }
            set { SetValue(LabelProperty, value); }
        }

        public static readonly DependencyProperty LabelProperty =
            DependencyProperty.Register("LabelProperty", typeof(string),
            typeof(PreferenceValueRadioButtonGroup), new PropertyMetadata("Preference", null));

        public PreferenceValueRadioButtonGroup()
        {
            InitializeComponent();
            Loaded += ControlLoaded;
        }

        private void ControlLoaded(object sender, RoutedEventArgs e)
        {
            // Must remove and reset dynamic controls for change binding to take place
            if (this.RadioButtonGroup.Children.Count != 0)
            {
                var count = this.RadioButtonGroup.Children.Count;
                var index = count-1;
                for (int i = 0; i < count; i++)
                {
                    var rb = this.RadioButtonGroup.Children[index--] as PreferenceValueRadioButton;
                    if (rb == null) continue;
                    this.RadioButtonGroup.Children.Remove(rb);
                    rb = null;
                }
            }
            Question.Text = Preference.Question;
            foreach (var pv in Preference.PreferenceValues)
            {
                var rb = new PreferenceValueRadioButton
                                {
                                    ButtonGroup = this,
                                    Label = pv.Answer,
                                    PreferenceValue = pv,
                                    EventAttendee = this.EventAttendee,
                                    DataContext = pv
                                };
                var pvId = pv.Id;
                rb.EventAttendeePreferenceValue =
                    this.EventAttendee.EventAttendeePreferenceValues.Where(eapv => eapv.PreferenceValues_Id == pvId)
                        .FirstOrDefault();
                this.RadioButtonGroup.Children.Add(rb);
            }
        }
    }
}
