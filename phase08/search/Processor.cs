using System.Collections.Generic;
using System.Linq;

namespace search
{
    public class Processor : ISearch
    {
        private readonly IDocumentRepository _documentRepository;

        public Processor(IDocumentRepository documentRepository)
        {
            _documentRepository = documentRepository;
        }

        public HashSet<string> Search(DocContainer docContainer)
        {
            var noSignDocsAndRemovedMinusDocs = GetNoSignDocs(docContainer.NoSignWords)
                .Where(x => !GetSignDocs(docContainer.MinusSignWords).Contains(x));
            return !docContainer.PlusSignWords.Any() ?
                noSignDocsAndRemovedMinusDocs.ToHashSet() :
                noSignDocsAndRemovedMinusDocs.Where(x => GetSignDocs(docContainer.PlusSignWords).Contains(x)).ToHashSet();
        }

        private IEnumerable<string> GetNoSignDocs(IEnumerable<string> noSignWords)
        {
            return noSignWords.Select(x => _documentRepository.GetDocsContain(x))
                .Aggregate((a, b) => a.Intersect(b).ToHashSet());
        }
        
        private HashSet<string> GetSignDocs(IEnumerable<string> signWords)
        {
            return signWords.SelectMany(x => _documentRepository.GetDocsContain(x)).ToHashSet();
        }
    }
}