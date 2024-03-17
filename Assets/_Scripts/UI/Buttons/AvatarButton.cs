using UnityEngine;
using UnityEngine.UI;
using Gamified.UI.Buttons;
using Gamified.Managers;

public class AvatarButton : ButtonBase
{
    [SerializeField] private Avatar _avatar;
    [SerializeField] private AvatarManager _avatarManager;
    [SerializeField] private Image _avatarDisplay;
    protected override void Awake()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(
                () => { OnClicked(); }
            );
    }
    protected override void OnClicked()
    {
        Debug.Log(name);
        _avatarManager.Init(_avatar, name);
        _avatarManager.Display();
    }
}
