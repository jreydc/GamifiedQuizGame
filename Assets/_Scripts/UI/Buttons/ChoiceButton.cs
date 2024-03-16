
using UnityEngine.UI;
using Gamified.UI.Buttons;

public class ChoiceButton : ButtonBase
{
    public static System.Action<string, Button> OnChoiceButtonClicked;
    protected override void OnClicked()
    {
        //base.OnClicked();
        if(_buttonLabel.text is not null)
        {
            OnChoiceButtonClicked?.Invoke(_buttonLabel.text, this.gameObject.GetComponent<Button>());
        }
    }
}
