using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gamified.UI;
using Gamified.Managers;

public class StartButton : ButtonBase
{
    protected override void OnClicked()
    {
        base.OnClicked();
        GameManager.Instance.ChangeState(GameState.Starting);
    }
}
