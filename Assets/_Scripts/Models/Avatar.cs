using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Default_Avatar")]
public class Avatar : ScriptableObject
{
    public int AvatarID;
    public string AvatarName;
    public Sprite AvatarIMG;
    public GameObject AvatarPrefab;
}
