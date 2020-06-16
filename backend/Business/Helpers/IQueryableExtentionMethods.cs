using System;
using System.Linq;
using backend.Data.Enums;
using backend.Data.Models;

namespace backend.Business.Helpers
{
    public static class QueryableExtensionMethods
    {
        public static IQueryable<Event> Filter(this IQueryable<Event> queryable, DateTime? date = null, int? eventTypeId = null, SeverityLevel? severityLevel = null)
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
    }
}