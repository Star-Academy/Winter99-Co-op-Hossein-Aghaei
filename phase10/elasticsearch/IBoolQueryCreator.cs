using Nest;

namespace elasticsearch
{
    public interface IBoolQueryCreator
    {
        QueryContainer CreateBoolQuery(DocContainer input);
    }
}