using System;
using UnityEngine;

public abstract class GameFlag<T> : GameFlag
{
    public T Value { get; private set; }

    private void OnEnable()
    {
        Value = default;
    }

    private void OnDisable()
    {
        Value = default;
    }

    public void Set(T value) {
        Value = value;
        GameFlagData.Value = Value.ToString();
        SendChanged();
    }
}

public abstract class GameFlag : ScriptableObject
{
    public GameFlagData GameFlagData { get; private set; }
    public event Action Changed;

    protected void SendChanged()
    {
        Changed?.Invoke();
    }

    public void Bind(GameFlagData gameFlagData) {
        this.GameFlagData = gameFlagData;
        SetFromData(gameFlagData.Value);
    }

    protected abstract void SetFromData(string value);
}

[Serializable]
public class GameFlagData {
    public string Name;
    public string Value;
}
