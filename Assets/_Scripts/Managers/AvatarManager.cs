using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AvatarManager : GenericSingleton<AvatarManager>
{
    [SerializeField] private RawImage _avatarIMGDisplay;
    private Avatar _avatar;
    public void SetAvatarIMG(Avatar avatar)
    {
        _avatar = avatar;
    }

    public void Display()
    {
        _avatarIMGDisplay.texture = _avatar.AvatarIMG;
    }
}
