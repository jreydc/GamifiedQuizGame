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
        GameManager.Instance.LoadLevel(SceneNames.AVATAR);
    }
}
