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
        Report report = log.GenerateReport();
        ShowReport(report);
    }


    private static void ShowReport(Report report)
    {
        Console.Clear();

        Console.WriteLine(report);

        Console.Read();
    }
}
