using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Gamified.UI.Buttons;

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

        Debug.Log(this.name);
        AvatarManager.Instance.SetAvatarIMG(_avatar);

    }
}
