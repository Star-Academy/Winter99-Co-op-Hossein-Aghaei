using elasticsearch.DocManager;
using Nest;

namespace elasticsearch.SearchConnection
{
    public interface IBoolQueryCreator
    {
        QueryContainer CreateBoolQuery(DocContainer input);
    }
}