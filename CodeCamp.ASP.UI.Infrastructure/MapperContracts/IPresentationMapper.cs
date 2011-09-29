namespace CodeCamp.ASP.UI.Infrastructure.Mappers
{
    using System.Collections.Generic;
    using CodeCamp.RIA.Data.Web;

    public interface IPresentationMapper
    {
        IList<Presentation> Map(List<EventPresentation> riaEventPresentations);
    }
}
