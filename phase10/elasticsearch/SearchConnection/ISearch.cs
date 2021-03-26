using System.Collections.Generic;

namespace elasticsearch
{
    public interface ISearch
    {
        HashSet<string> Search(DocContainer docContainer);
    }
}