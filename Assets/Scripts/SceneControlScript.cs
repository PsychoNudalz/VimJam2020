using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneControlScript : MonoBehaviour
{
    public virtual void loadScene(int i)
    {
        SceneManager.LoadScene(i);
    }

    public virtual void returnToQuestBoard()
    {
        loadScene(1);
    }

    public virtual void returnToMainMenu()
    {
        loadScene(0);
    }
}
