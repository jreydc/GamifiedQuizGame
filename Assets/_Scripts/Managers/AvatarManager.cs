using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Gamified.Managers
{
    public class AvatarManager : GenericSingleton<AvatarManager>
    {
        [SerializeField] private Image _avatarIMGDisplay;
        [SerializeField] private Avatar _defaultAvatar;
        public Avatar _avatar;

        public Avatar Avatar { get => _avatar; }

        protected override void Awake()
        {
            base.Awake();
            _avatar = _defaultAvatar;
        }

        public void Init(Avatar avatar)
        {
            _avatar = avatar;
        }

        public void Display()
        {
            _avatarIMGDisplay.sprite = _avatar.AvatarIMG;
        }
    }
}

