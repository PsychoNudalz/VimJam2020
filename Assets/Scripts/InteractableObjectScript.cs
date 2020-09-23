using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObjectScript : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;


    public void setOutline(float i,Color c)
    {
        spriteRenderer.material.SetFloat("_Outline", i);
        spriteRenderer.material.SetColor("_Colour", c);
    }

    public virtual bool activeObject()
    {
        return true;
    }
}
