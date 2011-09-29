namespace CodeCamp.ASP.UI.Infrastructure
{
    using CodeCamp.RIA.Data.Web;

    public interface ICodeCampAuthManager
    {
        void AddPerson(Person person);

        Person FindPerson(Person person);
    }
}
