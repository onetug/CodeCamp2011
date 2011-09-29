using System.Data.Objects.DataClasses;
using CodeCamp.RIA.Data.Web;

namespace CodeCamp.ASP.Web.UI.Infrastructure.MapperContracts
{
    public interface ISpeakerMapper
    {
        EntityCollection<Person> Map(EntityCollection<Person> speakers);
    }
}