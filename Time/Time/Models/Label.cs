namespace Time.Models;
internal class Label
{
    public Label(string value)
    {
        Value = value;
    }


    public string Value { get; }


    public static bool operator ==(Label a, Label b)
    {
        return a.Value == b.Value;
    }
    public static bool operator !=(Label a, Label b)
    {
        return !(a == b);
    }

    public override bool Equals(object? obj)
    {
        return obj is Label label && label == this;
    }

    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }

    public override string ToString()
    {
        return Value;
    }
}
