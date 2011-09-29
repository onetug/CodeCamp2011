namespace CodeCamp.RIA.UI.Model
{
    using System.Collections.ObjectModel;

    public class Profile : BaseModel
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
                }
            }
        }

        private string firstName;
        public string FirstName
        {
            get
            {
                return firstName;
            }
            set
            {
                if (firstName != value)
                {
                    firstName = value;
                    NotifyOfPropertyChange(() => FirstName);
                }
            }
        }
        private string lastName;
        public string LastName
        {
            get
            {
                return lastName;
            }
            set
            {
                if (lastName != value)
                {
                    lastName = value;
                    NotifyOfPropertyChange(() => LastName);
                }
            }
        }

        private string email;
        public string Email
        {
            get
            {
                return email;
            }
            set
            {
                if (email != value)
                {
                    email = value;
                    NotifyOfPropertyChange(() => Email);
                }
            }
        }

        private string title;
        public string Title
        {
            get
            {
                return title;
            }
            set
            {
                if (title != value)
                {
                    title = value;
                    NotifyOfPropertyChange(() => Title);
                    IsDirty = true;
                }
            }
        }

        private string bio;
        public string Bio
        {
            get
            {
                return bio;
            }
            set
            {
                if (bio != value)
                {
                    bio = value;
                    NotifyOfPropertyChange(() => Bio);
                    IsDirty = true;
                }
            }
        }

        private string website;
        public string Website
        {
            get
            {
                return website;
            }
            set
            {
                if (website != value)
                {
                    website = value;
                    NotifyOfPropertyChange(() => Website);
                    IsDirty = true;
                }
            }
        }

        private string blog;
        public string Blog
        {
            get
            {
                return blog;
            }
            set
            {
                if (blog != value)
                {
                    blog = value;
                    NotifyOfPropertyChange(() => Blog);
                    IsDirty = true;
                }
            }
        }

        private string twitter;
        public string Twitter
        {
            get
            {
                return twitter;
            }
            set
            {
                if (twitter != value)
                {
                    twitter = value;
                    NotifyOfPropertyChange(() => Twitter);
                    IsDirty = true;
                }
            }
        }

        private string passwordHash;
        public string PasswordHash
        {
            get
            {
                return passwordHash;
            }
            set
            {
                if (passwordHash != value)
                {
                    passwordHash = value;
                    NotifyOfPropertyChange(() => PasswordHash);
                    IsDirty = true;
                }
            }
        }

        private string imageUrl;
        public string ImageUrl
        {
            get
            {
                return imageUrl;
            }
            set
            {
                if (imageUrl != value)
                {
                    imageUrl = value;
                    NotifyOfPropertyChange(() => ImageUrl);
                    IsDirty = true;
                }
            }
        }

        private ObservableCollection<PreferenceValue> preferenceValues = new ObservableCollection<PreferenceValue>();
        public ObservableCollection<PreferenceValue> PreferenceValues
        {
            get
            {
                return preferenceValues;
            }
            set
            {
                if (preferenceValues != value)
                {
                    preferenceValues = value;
                    NotifyOfPropertyChange(() => PreferenceValues);
                    IsDirty = true;
                }
            }
        }

    }
}
