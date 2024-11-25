using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Time.Application;
using Time.Models;

namespace Time.Commands;
internal class ShowReportCommand : CommandBase
{
    public ShowReportCommand()
        : base("report")
    {
    }


    public override void Apply(Log log)
    {
        Report report = new(log.Entries, log.ExtraSegments);
        TimeOnly? endTime = log.CalculateEndTime(report);
        ShowReport(report, endTime);
    }


    private static void ShowReport(Report report, TimeOnly? endTime)
    {
        Console.Clear();

        if (endTime is not null)
        {
            Console.WriteLine($"Projected end of workday: {endTime}");
        }

        Console.WriteLine(report);

        Console.Read();
    }
}
