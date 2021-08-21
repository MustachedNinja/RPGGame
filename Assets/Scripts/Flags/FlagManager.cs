using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class FlagManager : MonoBehaviour
{
    [SerializeField] private List<GameFlag> _allFlags;
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
        }
    }
}