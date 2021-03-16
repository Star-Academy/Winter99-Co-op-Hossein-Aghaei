using Nest;

namespace elasticsearch
{
    public interface IBoolQuery
    {
        QueryContainer CreateBoolQuery(DocContainer input);
    }
}