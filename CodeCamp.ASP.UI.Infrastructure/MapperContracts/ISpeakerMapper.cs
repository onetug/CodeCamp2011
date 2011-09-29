namespace CodeCamp.ASP.UI.Infrastructure.MapperContracts
{
    using System.Data.Objects.DataClasses;
    using CodeCamp.RIA.Data.Web;

    public interface ISpeakerMapper
    {
        EntityCollection<Person> Map(EntityCollection<Person> speakers);
    }
}