namespace Time.Parsing;
internal interface IParser<T>
{
    T Parse(string word);
}
