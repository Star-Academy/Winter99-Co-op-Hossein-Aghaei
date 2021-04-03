using System.Collections.Generic;

namespace elasticsearch
{
    public interface IView
    {
        string GetUserInput();
        void ShowSearchResult(HashSet<string> searchResult);
    }
}