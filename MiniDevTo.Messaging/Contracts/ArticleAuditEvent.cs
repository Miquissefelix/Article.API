using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniDevTo.Messaging.Contracts;

    public record ArticleAuditEvent(Guid ArticleId, string Action, DateTime Timestamp, string PerformedBy);
