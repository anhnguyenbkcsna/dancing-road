using System;
using System.Collections.Generic;

[Serializable]
public class Wrapper
{
    public List<BeatMapEvent> events;
}

[Serializable]
public class BeatMapEvent
{
    public string type;
    public int tick;
    public int lane;
    public string color;
}

[Serializable]
public class BeatMapData
{
    public List<BeatMapEvent> events;
}