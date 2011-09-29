namespace CodeCamp.ASP.Web.UI.Infrastructure.ServiceContracts
{
    using System.Collections.Generic;
    using CodeCamp.RIA.Data.Web;
    using CodeCamp.ASP.Web.UI.Infrastructure.MapperContracts;
    using CodeCamp.ASP.Web.UI.Infrastructure.Mappers;
    
    public class PresentationMapper : IPresentationMapper
    {
        ISpeakerMapper SpeakerMapper { get; set; }

        public IList<Presentation> Map(List<EventPresentation> riaEventPresentations)
        {
            var consumerPresentation = new List<Presentation>();          
            
            riaEventPresentations.ForEach(p => consumerPresentation.Add(new Presentation
                                                                            {
                                                                                Description = p.Presentation.Description,
                                                                                Name = p.Presentation.Name,
                                                                                Level = p.Presentation.Level,
                                                                                PresentationSpeakers = p.Presentation.PresentationSpeakers
                                                                                // PresentationSpeakers has a Person and a Presentation
                                                                                //Speakers = this.SpeakerMapper.Map(p.Presentation.Speakers)
                                                                            }));

            return new List<Presentation>();   
        }        
    }
}