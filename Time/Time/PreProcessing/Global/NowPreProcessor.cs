namespace Time.PreProcessing.Global;
internal class NowPreProcessor : IPreProcessor
{
    public string Process(string word)
    {
        if (word is "." or ".now")
        {
            word = DateTime.Now.ToString("HH:mm");
        }

        return word;
    }
}
