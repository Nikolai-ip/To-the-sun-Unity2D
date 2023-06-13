using System;
using System.Collections.Generic;

[Serializable]
public class GameData
{
    public List<bool> CheckpointsData;

    public GameData()
    {
        CheckpointsData = new List<bool>();
    }
}
