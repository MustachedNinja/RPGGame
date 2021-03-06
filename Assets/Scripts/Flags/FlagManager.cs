using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class FlagManager : MonoBehaviour
{
    private GameFlag[] _allFlags;
    private Dictionary<string, GameFlag> _flagsByName;

    public static FlagManager Instance { get; private set; }

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        _flagsByName = _allFlags.ToDictionary(k => k.name.Replace(" ", ""), v => v);
    }

    private void OnValidate() {
        _allFlags = Extensions.GetAllInstances<GameFlag>();
    }

    public void Set(string flagName, string value)
    {
        if (_flagsByName.TryGetValue(flagName, out GameFlag flag) == false)
        {
            Debug.LogError($"Flag not found {flagName}");
            return;
        }
        else
        {
            if (flag is IntGameFlag intGameFlag)
            {
                if (int.TryParse(value, out var intGameValue))
                {
                    intGameFlag.Set(intGameValue);
                }
            }
            else if (flag is BoolGameFlag boolGameFlag)
            {
                if (bool.TryParse(value, out var boolGameValue)) {
                    boolGameFlag.Set(boolGameValue);
                }
            }
            else if (flag is DecimalGameFlag decimalGameFlag) {
                if (decimal.TryParse(value, out var decimalGameValue)) {
                    decimalGameFlag.Set(decimalGameValue);
                }
            }
            else if (flag is StringGameFlag stringGameFlag)
            {
                stringGameFlag.Set(value);
            }
        }
    }

    public void Bind(List<GameFlagData> gameFlagDatas) {
        foreach (GameFlag flag in _allFlags) {
            GameFlagData data = gameFlagDatas.FirstOrDefault(t => t.Name == flag.name);
            if (data == null) {
                data = new GameFlagData() { Name = flag.name };
                gameFlagDatas.Add(data);
            }
            flag.Bind(data);
        }
    }
}
