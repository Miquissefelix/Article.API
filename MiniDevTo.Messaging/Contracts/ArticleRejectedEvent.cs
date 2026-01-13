

namespace MiniDevTo.Messaging.Contracts;
public record ArticleRejectedEvent(Guid ArticleId, string Title, string AuthorEmail, string Reason);