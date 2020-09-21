using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class DamagePopUpScript : MonoBehaviour
{
    public TextMeshPro textMeshPro;
    public Animator animator;
    public Color textColour;



    public void setText(string s, Color c)
    {
        textColour = c;
        setText(s);
    }

    void setText(string s)
    {
        textMeshPro.text = s;
        textMeshPro.color = textColour;
    }

    private void OnEnable()
    {
        animator.Play("DamagePopUp_Play");
        StartCoroutine(disableWhenFinished());
    }



    IEnumerator disableWhenFinished()
    {
        yield return new WaitForSeconds(animator.GetCurrentAnimatorClipInfo(0).Length);
        gameObject.SetActive(false);
    }
}
