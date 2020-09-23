
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TurnOrderPieceScript : MonoBehaviour
{
    public Image image;
    public TextMeshProUGUI text;

    public void setTurn(Sprite s, string t)
    {
        image.sprite = s;
        text.text = t;
    }
}
