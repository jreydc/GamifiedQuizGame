using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerDetails : MonoBehaviour
{
    [SerializeField] private TMP_Text name;
    [SerializeField] private Image image;
    // Start is called before the first frame update
    private void Awake()
    {
        name = GameObject.Find("PlayerNickName").GetComponent<TMP_Text>();
        image = GameObject.Find("AvatarIMG").GetComponent<Image>();
    }

    private void Start()
    {
        DisplayDetails();
    }

    private void DisplayDetails()
    {
        image.sprite = LevelManager.Instance.Player.PlayerAvatar.AvatarIMG;
        name.text = LevelManager.Instance.Player.PlayerNickName;
    }
}
