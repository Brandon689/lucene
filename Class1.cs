using Lucene.Net.Analysis.Standard;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.Store;
using Lucene.Net.Util;

class SentenceIndexer
{
    public void IndexSentences(List<string> sentences, string indexPath)
    {
        var directory = FSDirectory.Open(new DirectoryInfo(indexPath));
        var analyzer = new StandardAnalyzer(LuceneVersion.LUCENE_48);
        var indexConfig = new IndexWriterConfig(LuceneVersion.LUCENE_48, analyzer);
        var writer = new IndexWriter(directory, indexConfig);

        foreach (var sentence in sentences)
        {
            var document = new Document();
            document.Add(new TextField("text", sentence, Field.Store.YES));
            writer.AddDocument(document);
        }

        writer.Flush(triggerMerge: false, applyAllDeletes: false);
        writer.Commit();
        writer.Dispose();
    }
}
