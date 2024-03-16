using UnityEngine;
using UnityEngine.UI;
using Gamified.UI.Buttons;

public class AvatarButton : ButtonBase
{
    [SerializeField] private Avatar _avatar;
    [SerializeField] private string buttonName => this.name;
    protected override void Awake()
    {
        _button = GetComponent<Button>();
        
    }
    protected override void OnClicked()
    {
        Debug.Log(this.name);
        //AvatarManager.Instance.SetAvatarIMG(_avatar);
    }
}
