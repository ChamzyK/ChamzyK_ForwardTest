namespace ChamzyK_ForwardTest.Engines
{
    public interface IEngine
    {
        double OverheatsT { get; set; }
        double T { get; set; }
        void StartEngine(double ambientT);


        void DoIteration();
    }
}
