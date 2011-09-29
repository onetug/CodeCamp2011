using System;
using System.Collections.ObjectModel;
using System.Linq;


namespace CodeCamp.RIA.UI.Model
{
    using CodeCamp.RIA.Data.Web;

    public class Presentation : BaseModel
    {
        private string name;
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                if (name != value)
                {
                    name = value;
                    NotifyOfPropertyChange(() => Name);
                    IsDirty = true;
                }
            }
        }
        private string description;
        public string Description
        {
            get
            {
                return description;
            }
            set
            {
                if (description != value)
                {
                    description = value;
                    NotifyOfPropertyChange(() => Description);
                    IsDirty = true;
                }
            }
        }

        private ObservableCollection<EventPresentation> eventPresentations;
        public ObservableCollection<EventPresentation> EventPresentations
        {
            get
            {
                return eventPresentations;
            }
            set
            {
                if (eventPresentations != value)
                {
                    eventPresentations = value;
                    NotifyOfPropertyChange(() => EventPresentations);
                    IsDirty = true;
                }
            }
        }

        private ObservableCollection<File> files;
        public ObservableCollection<File> Files
        {
            get
            {
                return files;
            }
            set
            {
                if (files != value)
                {
                    files = value;
                    NotifyOfPropertyChange(() => Files);
                    IsDirty = true;
                }
            }
        }

        private string level;
        public string Level
        {
            get
            {
                return level;
            }
            set
            {
                if (level != value)
                {
                    level = value;
                    NotifyOfPropertyChange(() => Level);
                    IsDirty = true;
                }
            }
        }

        private ObservableCollection<PresentationSpeaker> presentationSpeakers;
        public ObservableCollection<PresentationSpeaker> PresentationSpeakers
        {
            get
            {
                return presentationSpeakers;
            }
            set
            {
                if (presentationSpeakers != value)
                {
                    presentationSpeakers = value;
                    NotifyOfPropertyChange(() => PresentationSpeakers);
                    IsDirty = true;
                }
            }
        }

        private string approvalStatus;
        public string ApprovalStatus
        {
            get
            {
                return approvalStatus;
            }
            set
            {
                if (approvalStatus != value)
                {
                    approvalStatus = value;
                    NotifyOfPropertyChange(() => ApprovalStatus);
                }
            }
        }

        public bool IsNotApproved
        {
            get { return ApprovalStatus != "Confirmed"; }
        }
    }
}
