using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayCode : MonoBehaviour
{
    public GameObject MainCamera;
    public GameObject PlayMusic;
    public GameObject TravelMusic;

    public GameObject Agent;

    //change le programme de la main camera, passant de la vue perspective a travel.
    public void PlayCam(bool PlayMode){
        MainCamera.SetActive(true);
        MainCamera.GetComponent<Travelling>().enabled = !PlayMode;
        TravelMusic.SetActive(!PlayMode);
        
        MainCamera.GetComponent<playermovement>().enabled = PlayMode;
        PlayMusic.SetActive(PlayMode);
        Agent.SetActive(PlayMode);
    }
}
