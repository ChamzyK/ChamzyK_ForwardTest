using ChamzyK_ForwardTest.Engines;

namespace ChamzyK_ForwardTest.TestBenches
{
    public interface ITestBench
    {
        IEngine Engine { get; set; }
        double StartBench(double ambientT);
    }
}
