using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Gamified.Managers
{
    public class AvatarManager : MonoBehaviour
    {
        [SerializeField] private Image _avatarIMGDisplay;
        [SerializeField] private Avatar _defaultAvatar;
        public static Avatar _avatar;

        private void Awake()
        {
            _avatar = _defaultAvatar;
        }

        public void Init(Avatar avatar, string avatarName)
        {
            _avatar = avatar;
        }

        public void Display()
        {
            _avatarIMGDisplay.sprite = _avatar.AvatarIMG;
        }
    }
}

