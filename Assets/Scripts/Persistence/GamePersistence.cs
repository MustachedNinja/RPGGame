using UnityEngine;

public class GamePersistence : MonoBehaviour
{

    private GameData _gameData;

    void Start()
    {
        LoadGameFlags();   
    }

    void OnDisable() {
        SaveGameFlags();
    }

    private void SaveGameFlags() {
        string json = JsonUtility.ToJson(_gameData);
    }

    private void LoadGameFlags() {
        _gameData = new GameData();
    }
}