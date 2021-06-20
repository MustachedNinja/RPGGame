using UnityEngine;

public class DialogGiver : MonoBehaviour
{
    [SerializeField] private TextAsset _dialog;
    [SerializeField] private bool _lookAt;

    void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            FindObjectOfType<DialogController>().StartDialog(_dialog);
            if (_lookAt) {
                transform.LookAt(other.transform);
            }
        }
    }
}
