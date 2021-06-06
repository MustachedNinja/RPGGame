using TMPro;
using Ink.Runtime;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class DialogController : MonoBehaviour
{
    [SerializeField] private TMP_Text _storyText;
    [SerializeField] private Button[] _choiceButtons;

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

}
