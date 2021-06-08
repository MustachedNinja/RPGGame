using UnityEngine;
using UnityEngine.Events;

public class GameEventListener : MonoBehaviour {

    [SerializeField] private UnityEvent _unityEvent;
    [SerializeField] private GameEvent _gameEvent;
    
    void Awake() {
        _gameEvent.Register(this);
    }

    void OnDisable() {
        _gameEvent.Deregister(this);
    }

    public void RaiseEvent() {
        _unityEvent.Invoke();
    }

}
