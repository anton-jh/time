using Time.Models;

namespace Time.Application;
internal class SubSegment
{
    public SubSegment(TimeSpan timeSpan, Label label, bool subtractive)
    {
        TimeSpan = timeSpan;
        Label = label;
        Subtractive = subtractive;
    }


    public TimeSpan TimeSpan { get; }
    public Label Label { get; }
    public bool Subtractive { get; }


    public override string ToString()
    {
        return $"{(Subtractive ? "-" : "+")} {TimeSpan.Hours}h {TimeSpan.Minutes}min ({Label})";
    }
}
