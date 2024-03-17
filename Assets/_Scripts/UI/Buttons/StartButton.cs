using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gamified.UI.Buttons;
using Gamified.Managers;

public class StartButton : ButtonBase
{
    protected override void OnClicked()
    {
        base.OnClicked();
        AudioManager.Instance.PlaySound(SoundNames.BUTTON);
        GameManager.Instance.LoadLevel(SceneNames.AVATAR);
        
    }
}
