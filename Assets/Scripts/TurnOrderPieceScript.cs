
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TurnOrderPieceScript : MonoBehaviour
{
    public Image image;
    public TextMeshProUGUI text;

    public void setTurn(SpriteRenderer s, string t)
    {
        image.sprite = s.sprite;
        image.color = s.color;
        image.material = s.material;
        text.text = t;
    }
}
