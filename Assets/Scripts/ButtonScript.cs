using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : SceneControlScript
{
    public GameObject gm;

    public Sound clickSound;

    public void toggleGM()
    {
        gm.SetActive(!gm.activeSelf);
    }

    public void playSound()
    {
        try
        {
            FindObjectOfType<SoundManager>().Play(clickSound);
        }catch(System.Exception e){

        }
    }

    public void RESETDATA()
    {
        SaveSystem.ResetData();
    }
}
