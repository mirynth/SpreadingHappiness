using System.Collections;
using System.Collections.Generic;

public class VNScene
{
    Queue<VNMoment> moments = new();

    public VNMoment PrepareMoment()
    {
        return VNMoment.Create();
    }

    public void AddMoment(VNMoment moment)
    {
        moments.Enqueue(moment);
    }

    public bool CanPop()
    {
        return moments.Count > 0;
    }

    public VNMoment Pop()
    {
        return moments.Dequeue();
    }
}