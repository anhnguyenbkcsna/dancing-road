using System;
using System.Collections.Generic;

[Serializable]
public class BeatMapEvent
{
    public string type;
    public int tick;
    public int lane;
    public BallColor color;
}

[Serializable]
public class BeatMapData
{
    public List<BeatMapEvent> events;
}