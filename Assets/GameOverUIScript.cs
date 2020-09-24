using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverUIScript : SceneControlScript
{
    public override void loadScene(int i)
    {
        base.loadScene(i);
    }

    public override void returnToQuestBoard()
    {
        PlayerManagerScript playerManagerScript = FindObjectOfType<GameManagerScript>().playerManagerScript;
        playerManagerScript.sellAllLoot();
        playerManagerScript.SavePlayer();
        playerManagerScript.deactiveUnits();

        loadScene(1);
    }
}
