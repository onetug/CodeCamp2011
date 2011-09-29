namespace CodeCamp.RIA.UI.ViewModels
{
    using System;
    using System.ComponentModel;
    using Caliburn.Micro;
    using CodeCamp.RIA.Data.Web;
    using CodeCamp.RIA.UI.Infrastructure;

  public class PresentationViewModel : Screen, IModule, IEditableObject
    {
        #region Fields
        private Model.Presentation copyData;
        private Model.Presentation currentData;
        #endregion

        #region Properties
        private string pageTitle;
        public string PageTitle
        {
            get
            {
                return pageTitle;
            }
            set
            {
                if (pageTitle != value)
                {
                    pageTitle = value;
                    NotifyOfPropertyChange(() => PageTitle);

                }
            }
        }


        private bool isBusy;
        public bool IsBusy
        {
            get
            {
                return isBusy;
            }
            set
            {
                if (isBusy != value)
                {
                    isBusy = value;
                    NotifyOfPropertyChange(() => IsBusy);

                }
            }
        }
        #endregion

        public PresentationViewModel()
        {
            PageTitle = "Add new Presentation";
            Presentation = new Model.Presentation { Id=0, Level = PresentationLevel.Level100.ToString().Replace("Level", "") };
            this.PresentationName = Presentation.Name;
            Description = Presentation.Description;
            Level = (PresentationLevel)Convert.ToInt32(Presentation.Level);
            mode = Mode.New;
        }

        public PresentationViewModel(Model.Presentation presentation)
        {
            PageTitle = "Edit " + presentation.Name;
            this.Presentation = presentation;
            this.PresentationName = Presentation.Name;
            Description = Presentation.Description;
            Level = (PresentationLevel)Convert.ToInt32(Presentation.Level);
            mode = Mode.Edit;
        }
        private Model.Presentation presentation;
        public Model.Presentation Presentation
        {
            get
            {
                return presentation;
            }
            set
            {
                if (presentation != value)
                {
                    presentation = value;
                    this.PresentationName = Presentation.Name;
                    Description = Presentation.Description;
                    Level = (PresentationLevel)Convert.ToInt32(Presentation.Level);
                    NotifyOfPropertyChange(() => Presentation);
                    NotifyOfPropertyChange(() => CanSave);

                }
            }
        }

        private string presentationName;
        public string PresentationName
        {
            get
            {
                return presentationName;
            }
            set
            {
                if (presentationName != value)
                {
                    presentationName = value;
                    NotifyOfPropertyChange(() => PresentationName);
                    NotifyOfPropertyChange(() => CanSave);

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
                    NotifyOfPropertyChange(() => CanSave);

                }
            }
        }

        private PresentationLevel level;
        public PresentationLevel Level
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
                    NotifyOfPropertyChange(() => CanSave);

                }
            }
        }

        private bool IsValid()
        {
            return !string.IsNullOrEmpty(this.PresentationName) &&
                   !string.IsNullOrEmpty(this.Description) &&
                   (Level.ToString() == PresentationLevel.Level100.ToString() ||
                    Level.ToString() == PresentationLevel.Level200.ToString() ||
                    Level.ToString() == PresentationLevel.Level300.ToString());
        }

        #region Commands
        public bool WasSaved { get; set; }
        public void Save()
        {
            //Deactivate(true);
            this.Presentation.Name = PresentationName;
            this.Presentation.Description = Description;
            this.Presentation.Level = Level.ToString().Replace("Level", "");
            WasSaved = true;
            TryClose();
        }

        public bool CanSave
        {
            get { return IsValid(); }

        }
        public bool WasCancelled {get; set;}
        public void Cancel()
        {
            //Deactivate(false);
            WasCancelled = true;
            TryClose();
        }

        public bool CanCancel
        {
            get { return true; }

        }
        #endregion

        #region Mode Enumeration

        private enum Mode { New, Edit }
        private Mode mode;
        public bool IsNewMode { get { return mode == Mode.New; } }
        public bool IsEditMode { get { return mode == Mode.Edit; } }

        #endregion

        #region IEditableObject Members

        public void BeginEdit()
        {
            copyData = currentData;
        }

        public void CancelEdit()
        {
            currentData = copyData;
            NotifyOfPropertyChange("");

        }

        public void EndEdit()
        {
            copyData = new Model.Presentation();
        }

        #endregion


        #region Deactivation

        //public event EventHandler<DeactivationEventArgs> AttemptingDeactivation;

        //public void Deactivate(bool close)
        //{
        //    var e = new DeactivationEventArgs { WasClosed = true };
        //    AttemptingDeactivation(this, e);
        //}

        //public event EventHandler<DeactivationEventArgs> Deactivated;


        #endregion
    }
}
