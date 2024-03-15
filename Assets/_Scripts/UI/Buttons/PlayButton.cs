using Gamified.UI.Buttons;
using Gamified.Managers;

public class PlayButton : ButtonBase
{
    protected override void OnClicked()
    {
        GameManager.Instance.ChangeState(GameState.InGame);
    }
}
