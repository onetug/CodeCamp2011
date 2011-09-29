namespace CodeCamp.ASP.UI.Infrastructure.ServiceContracts
{
    using System.Collections.Generic;
    using CodeCamp.RIA.Data.Web;
    using CodeCamp.ASP.UI.Infrastructure.MapperContracts;
    using CodeCamp.ASP.UI.Infrastructure.Mappers;
    
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
                                                                                // PresentationSpeakers has Person and Presentation
                                                                                //Speakers = this.SpeakerMapper.Map(p.Presentation.Speakers)
                                                                            }));

            return new List<Presentation>();   
        }        
    }
}