using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CodeCamp.RIA.Data.Web;

namespace CodeCamp.ASP.Web.UI.Infrastructure.Mappers
{
    public interface IPresentationMapper
    {
        IList<Presentation> Map(List<EventPresentation> riaEventPresentations);
    }
}
