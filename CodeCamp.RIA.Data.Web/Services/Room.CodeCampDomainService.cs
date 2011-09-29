
namespace CodeCamp.RIA.Data.Web
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Data;
    using System.Linq;
    using System.ServiceModel.DomainServices.EntityFramework;
    using System.ServiceModel.DomainServices.Hosting;
    using System.ServiceModel.DomainServices.Server;
    using CodeCamp.RIA.Data.Web;


    // Implements application logic using the CodeCampModelContainer context.
    // TODO: Add your application logic to these methods or in additional methods.
    // TODO: Wire up authentication (Windows/ASP.NET Forms) and uncomment the following to disable anonymous access
    // Also consider adding roles to restrict access as appropriate.
    // [RequiresAuthentication]
    //[EnableClientAccess()]
    public partial class CodeCampDomainService : LinqToEntitiesDomainService<CodeCampModelContainer>
    {

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Rooms' query.
        [Query(IsDefault = true)]
        [OutputCache(OutputCacheLocation.Server, duration: 60 * 60)] // one hour of caching
        public IQueryable<Room> GetRooms()
        {
            return this.ObjectContext.Rooms;
        }

        [Query]
        [OutputCache(OutputCacheLocation.Server, duration: 60 * 60)] // one hour of caching
        public IQueryable<Room> GetRoomsForLocation(int locationId)
        {
            return this.ObjectContext.Rooms.Where(r => r.LocationId == locationId);
        }

        public Room GetRoom(int id)
        {
            return this.ObjectContext.Rooms.Where(r => r.Id == id).FirstOrDefault();
        }
        [Insert]
        public void InsertRoom(Room room)
        {
            if ((room.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(room, EntityState.Added);
            }
            else
            {
                this.ObjectContext.Rooms.AddObject(room);
            }
        }

        [Update]
        public void UpdateRoom(Room currentRoom)
        {
            this.ObjectContext.Rooms.AttachAsModified(currentRoom, this.ChangeSet.GetOriginal(currentRoom));
        }

        [Delete]
        public void DeleteRoom(Room room)
        {
            if ((room.EntityState == EntityState.Detached))
            {
                this.ObjectContext.Rooms.Attach(room);
            }
            this.ObjectContext.Rooms.DeleteObject(room);
        }
    }
}


