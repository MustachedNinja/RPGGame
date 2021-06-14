using TMPro;
using Ink.Runtime;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class DialogController : MonoBehaviour
{
    [SerializeField] private TMP_Text _storyText;
    [SerializeField] private Button[] _choiceButtons;
    [SerializeField] private Animator _animator;

    Story _story;

    [ContextMenu("Start Dialog")]
    public void StartDialog(TextAsset dialog) {
        _story = new Story(dialog.text);
        RefreshView();
    }

    private void RefreshView() {
        StringBuilder storyTextBuilder = new StringBuilder();
        while(_story.canContinue) {
            storyTextBuilder.AppendLine(_story.Continue());
            HandleTags();
        }

        _storyText.SetText(storyTextBuilder);

        for (int i = 0; i < _choiceButtons.Length; i++) {
            Button button = _choiceButtons[i];
            button.onClick.RemoveAllListeners();
            button.gameObject.SetActive(i < _story.currentChoices.Count);
            if (i < _story.currentChoices.Count) {
                Choice choice = _story.currentChoices[i];
                button.GetComponentInChildren<TMP_Text>().SetText(choice.text);
                button.onClick.AddListener(() => {
                    _story.ChooseChoiceIndex(choice.index);
                    RefreshView();
                });
            }
        }
    }

    private void HandleTags() {
        foreach(string tag in _story.currentTags) {
            if (tag.StartsWith("E.")) {
                string eventName = tag.Remove(0, 2);
                GameEvent.RaiseEvent(eventName);
            }
        }
    }
}