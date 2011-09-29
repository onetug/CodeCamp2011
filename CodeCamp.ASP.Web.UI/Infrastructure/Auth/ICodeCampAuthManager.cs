namespace CodeCamp.ASP.Web.UI.Auth
{
    using CodeCamp.RIA.Data.Web;

    public interface ICodeCampAuthManager
    {
        void AddPerson(Person person);

        Person FindPerson(Person person);
    }
}
