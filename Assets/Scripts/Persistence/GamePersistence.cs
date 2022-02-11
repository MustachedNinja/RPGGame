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
        PlayerPrefs.SetString("GameData", json);
    }

    private void LoadGameFlags() {
        string json = PlayerPrefs.GetString("GameData");
        _gameData = JsonUtility.FromJson<GameData>(json);
        if (_gameData == null) {
            _gameData = new GameData();
        }
        FlagManager.Instance.Bind(_gameData.GameFlagDatas);
    }
}