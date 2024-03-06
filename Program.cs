var sentences = new List<string>
{
    "This is a sample sentence.",
    "Lucene is a powerful search library.",
    "Searching sentences with Lucene is efficient.",
};

int j = 0;
for (int i = 0; i < 100000; i++)
{
    sentences.Add(sentences[j] + i.ToString());
    if (j == 2) j = 0;
}

var indexPath = "../../domo";
var indexer = new SentenceIndexer();
indexer.IndexSentences(sentences, indexPath);

var searcher = new SentenceSearcher();
var searchResults = searcher.SearchSentences("search j", indexPath);

foreach (var result in searchResults)
{
    Console.WriteLine(result);
}
