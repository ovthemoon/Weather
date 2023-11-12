using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageClear : MonoBehaviour
{
    public string mapName;
    private void OnMouseDown()
    {
        if (mapName.Equals("Desert"))
        {
            GameManager.map2_DesertComplete = true;
        }
        else if (mapName.Equals("Pole"))
        {
            GameManager.map2_PoleComplete = true;
        }
    }
}
