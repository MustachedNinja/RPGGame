using UnityEngine;

public class DialogGiver : MonoBehaviour
{
    [SerializeField] private TextAsset _dialog;

    void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            FindObjectOfType<DialogController>().StartDialog(_dialog);
            transform.LookAt(other.transform);
        }
    }
}
