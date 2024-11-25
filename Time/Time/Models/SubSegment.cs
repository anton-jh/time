namespace Time.Models;
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

    public string Serialize()
    {
        return $"{(Subtractive ? "-" : "+")}{TimeSpan.Hours}h{TimeSpan.Minutes}min,{Label}";
    }
}
// TODO: separate into SubSegment (always subtractive) and ExtraSegment (always additive).
// THE PLAN: build a system that handles "log lines" and navigating and manipulating the list of log lines and acts on the Log class.
// TODO: Replace ToString for displaying entities to the user with a method that returns a list of primitives (maybe just strings) to display as multiple selectable lines.
// MAYBE: Completely replace the core logic (Log etc) with something that natively supports discrete lines and editing?