using System.Collections.Generic;

namespace search
{
    public interface ISearch
    {
        HashSet<string> Search(DocContainer docContainer);
    }
}