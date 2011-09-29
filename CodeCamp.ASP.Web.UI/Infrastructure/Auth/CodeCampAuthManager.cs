using CodeCamp.ASP.Web.UI.Infrastructure.ServiceContracts;

namespace CodeCamp.ASP.Web.UI.Auth
{
    using System;
    using CodeCamp.RIA.Data.Web;

    public class CodeCampAuthManager : ICodeCampAuthManager
    {
        ICodeCampDataService CodeCampDataService { get; set; }

        public CodeCampAuthManager(ICodeCampDataService codeCampDataService)
        {
            this.CodeCampDataService = codeCampDataService;
        }

        public void AddPerson(Person person)
        {
            if (!ValidatePerson(person))
            this.CodeCampDataService.AddPerson(person);    
        }

        private static bool ValidatePerson(Person person)
        {
            if (!String.IsNullOrEmpty(person.Email))
            {
                throw new CodeCampAuthorizationException("Email is null or empty");
            }

            if(!String.IsNullOrEmpty(person.Name))
            {
                throw new CodeCampAuthorizationException("Name is null or empty");
            }

            if(!String.IsNullOrEmpty(person.PasswordHash))
            {
                throw new CodeCampAuthorizationException("PasswordHash is null or empty");
            }
            
            return true;
        }

        public Person FindPerson(Person person)
        {
            if (String.IsNullOrEmpty(person.Email))
            {
                throw new CodeCampAuthorizationException("Email is missing");
            }
            return this.CodeCampDataService.FindPersonByEmail(person);
        }
    }
}