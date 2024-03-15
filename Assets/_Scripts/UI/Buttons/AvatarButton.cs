using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Gamified.UI;

public class AvatarButton : ButtonBase
{
    [SerializeField] private Avatar _avatar;

    protected override void Awake()
    {
        base.Awake();
    }
    protected override void OnClicked()
    {
        base.OnClicked();

    }
}
