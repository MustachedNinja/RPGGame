using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFlagTriggerAreaForIntFlags : MonoBehaviour {
    [SerializeField] private int _amount;
    [SerializeField] private IntGameFlag _gameFlag;

    private void OnTriggerEnter(Collider other) {
        _gameFlag.Modify(_amount);
    }
}
