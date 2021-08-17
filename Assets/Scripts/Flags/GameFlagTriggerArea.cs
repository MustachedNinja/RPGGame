using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFlagTriggerArea : MonoBehaviour
{

    [SerializeField] private BoolGameFlag _gameFlag;

    private void OnTriggerEnter(Collider other) {
        _gameFlag.Set(true);
    }
}