namespace CodeCamp.RIA.UI.Model
{
    using CodeCamp.RIA.UI.Events;
    using Caliburn.Micro;

    public class BaseModel: PropertyChangedBase
    {
        private int id;
        public int Id
        {
            get
            {
                return id;
            }
            set
            {
                if (id!= value)
                {
                    id = value;
                    NotifyOfPropertyChange(() => Id);
                }
            }
        }

        private bool isDirty;
        public bool IsDirty
        {
            get
            {
                return isDirty;
            }
            set
            {
                if (isDirty != value)
                {
                    isDirty = value;
                    NotifyOfPropertyChange(() => IsDirty);
                }
                if (EventAggregator != null)
                    EventAggregator.Publish(new IsDirtyEvent { DirtyState = value });
            }
        }
        public IEventAggregator EventAggregator { get; set; }
    }
}
