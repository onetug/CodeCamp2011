namespace CodeCamp.RIA.UI.Model
{
    using Caliburn.Micro;


    public class PreferenceValue : BaseModel
    {

        private int profileId;
        public int ProfileId
        {
            get
            {
                return profileId;
            }
            set
            {
                if (profileId != value)
                {
                    profileId = value;
                    NotifyOfPropertyChange(() => ProfileId);
                    IsDirty = true;
                }
            }
        }

        private int preferenceValueId;
        public int PreferenceValueId
        {
            get
            {
                return preferenceValueId;
            }
            set
            {
                if (preferenceValueId != value)
                {
                    preferenceValueId = value;
                    NotifyOfPropertyChange(() => PreferenceValueId);
                    IsDirty = true;
                }
            }
        }

        private int preferenceId;
        public int PreferenceId
        {
            get
            {
                return preferenceId;
            }
            set
            {
                if (preferenceId != value)
                {
                    preferenceId = value;
                    NotifyOfPropertyChange(() => PreferenceId);
                    IsDirty = true;
                }
            }
        }

    }
}
