using UnityEngine;
using TMPro;

public class LevelManager : GenericSingleton<LevelManager>
{
    [SerializeField] private TMP_InputField nickName;
    private string playerNickName;
    private Player _player;

    public Player Player { get => _player; }

    private void Start()
    {
        nickName = nickName.GetComponent<TMP_InputField>();
    }

    public void PreInitPlayerName()
    {
        _player = new Player();
        playerNickName = nickName.text;
        if (nickName != null)
        {
            _player.PlayerNickName = playerNickName;
        }
        else
        {
            Debug.LogError("nickName is not assigned!");
        }
    }
    public void PreInitAvatar(Avatar playerAvatar)
    {
        
        _player.PlayerAvatar = playerAvatar;
    }

    public void SetAvatarFromAvatarMNGR()
    {

    }
}
