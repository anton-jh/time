namespace Time.PreProcessing.Time;
internal class ColonTimePreProcessor : IPreProcessor
{
    public string Process(string word)
    {
        if (word.Length == 4)
        {
            word = $"{word[..2]}:{word[2..4]}";
        }

        return word;
    }
}
