using Gamified.UI.Buttons;
using Gamified.Managers;

public class MainMenuButton : ButtonBase
{
    protected override void OnClicked()
    {
        GameManager.Instance.LoadLevel(SceneNames.MAINMENU);
    }
}
