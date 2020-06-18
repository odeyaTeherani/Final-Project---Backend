using System;
using System.Linq;
using backend.Data.Enums;
using backend.Data.Models;

namespace backend.Business.Helpers
{
    public static class QueryableExtensionMethods
    {
        public static IQueryable<Event> FilterEvents(this IQueryable<Event> queryable, DateTime? date = null, int? eventTypeId = null, SeverityLevel? severityLevel = null)
        {
            if (date.HasValue)
            {
                queryable = queryable.Where(x =>
                    x.StartDate.Date <= date.Value.Date &&
                    date.Value.Date <= x.EndDate.Value.Date);
            }
            
            if (eventTypeId.HasValue)
            {
                queryable = queryable.Where(x =>
                    x.EventTypeId.HasValue && x.EventTypeId.Value == eventTypeId.Value);
            }
            
            if (severityLevel.HasValue)
            {
                queryable = queryable.Where(x =>
                    x.SeverityLevelType.HasValue && x.SeverityLevelType.Value == severityLevel.Value);
            }

            return queryable;
        }
        
        public static IQueryable<ApplicationUser> FilterUsers(this IQueryable<ApplicationUser> queryable, string name = null, string email = null, int? subRoleId = null)
        {
            if (name != null)
            {
                queryable = queryable.Where(x =>
                    x.FirstName == name || x.LastName == name);
            }
            
            if (email != null)
            {
                queryable = queryable.Where(x =>
                    x.Email == email);
            }
            
            if (subRoleId.HasValue)
            {
                queryable = queryable.Where(x =>
                    x.SubRoleId == subRoleId);
            }

            return queryable;
        }
    }
}