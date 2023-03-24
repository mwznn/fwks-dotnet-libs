namespace Fwks.Core.Domain;

public abstract record BasePageQuery(int CurrentPage = 1, int PageSize = 10);