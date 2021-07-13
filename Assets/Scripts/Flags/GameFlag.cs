using UnityEngine;

[CreateAssetMenu(menuName = "Bool Game Flag")]
public class GameFlag : ScriptableObject
{
    public bool Value;

    private void OnEnable() {
        Value = default;
    }
}
