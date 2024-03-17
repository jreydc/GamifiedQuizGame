using Gamified.UI.Buttons;
using Gamified.Managers;

public class TryAgainButton : ButtonBase
{
    protected override void OnClicked()
    {
        GameManager.Instance.ChangeState(GameState.InGame);
    }
}
