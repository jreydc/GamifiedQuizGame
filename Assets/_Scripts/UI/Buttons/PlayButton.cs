using Gamified.UI.Buttons;
using Gamified.Managers;

public class PlayButton : ButtonBase
{
    protected override void OnClicked()
    {
        AudioManager.Instance.PlaySound(SoundNames.BUTTON);
        LevelManager.Instance.PreInitPlayerName();
        LevelManager.Instance.PreInitAvatar(AvatarManager.Instance.Avatar);
        GameManager.Instance.LoadLevel(SceneNames.IN_GAME);
    }
}
