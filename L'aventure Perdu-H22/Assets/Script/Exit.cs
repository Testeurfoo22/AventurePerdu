using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Exit : MonoBehaviour
{
    //quitte le jeu
    public void EndGame(){
        Application.Quit();
        Debug.Log("Quit");
    }
}
