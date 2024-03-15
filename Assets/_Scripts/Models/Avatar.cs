using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Default_Avatar")]
public class Avatar : ScriptableObject
{
    public int PlayerID;
    public string PlayerName;
    public Image AvatarIMG;
}
