using System.Collections.Generic;
using elasticsearch.DocManager;

namespace elasticsearch.SearchConnection
{
    public interface ISearch
    {
        HashSet<string> Search(DocContainer docContainer);
    }
}