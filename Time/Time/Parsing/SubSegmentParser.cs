using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Time.Application;
using Time.Exceptions;
using Time.Models;

namespace Time.Parsing;
internal partial class SubSegmentParser : IParser<SubSegment>
{
    public SubSegment Parse(string word)
    {
        Match match = Pattern().Match(word);

        if (!match.Success)
        {
            throw new UserErrorException("Invalid segment.");
        }

        string modeString = match.Groups[1].Value;
        string hoursString = match.Groups[2].Value;
        string minutesString = match.Groups[3].Value;
        string labelString = match.Groups[4].Value;

        bool subtractive = modeString is "-";
        int hours = string.IsNullOrWhiteSpace(hoursString) ? 0 : int.Parse(hoursString);
        int minutes = string.IsNullOrWhiteSpace(minutesString) ? 0 : int.Parse(minutesString);
        Label label = new(labelString);

        TimeSpan timeSpan = new(hours, minutes, 0);

        return new SubSegment(timeSpan, label, subtractive);
    }


    [GeneratedRegex("^([-+])(?:(\\d+)h)?(?:(\\d+)m(?:in)?)?,(.+)$")]
    private static partial Regex Pattern();
}
