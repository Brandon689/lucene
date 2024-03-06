using Lucene.Net.Analysis.Standard;
using Lucene.Net.Index;
using Lucene.Net.QueryParsers.Classic;
using Lucene.Net.Search;
using Lucene.Net.Store;
using Lucene.Net.Util;

class SentenceSearcher
{
    public List<string> SearchSentences(string query, string indexPath)
    {
        var directory = FSDirectory.Open(new DirectoryInfo(indexPath));
        var indexReader = DirectoryReader.Open(directory);
        var indexSearcher = new IndexSearcher(indexReader);

        var parser = new QueryParser(LuceneVersion.LUCENE_48, "text", new StandardAnalyzer(LuceneVersion.LUCENE_48));
        var luceneQuery = parser.Parse(query);

        var topDocs = indexSearcher.Search(luceneQuery, 10);

        var results = new List<string>();

        foreach (var scoreDoc in topDocs.ScoreDocs)
        {
            var document = indexSearcher.Doc(scoreDoc.Doc);
            var sentence = document.Get("text");
            results.Add(sentence);
        }

        indexReader.Dispose();
        directory.Dispose();

        return results;
    }
}
