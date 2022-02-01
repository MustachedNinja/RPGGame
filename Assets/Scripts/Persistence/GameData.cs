using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameData {

    public List<GameFlagData> GameFlagDatas;

    public GameData() {
        GameFlagDatas = new List<GameFlagData>();
        GameFlagDatas.Add(new GameFlagData() { Value = "james", Name = "flagname" });
    }
}