using System.Collections.Generic;

namespace search
{
    public interface IView
    {
        string GetUserInput();
        void ShowSearchResult(HashSet<string> searchResult);
    }
}