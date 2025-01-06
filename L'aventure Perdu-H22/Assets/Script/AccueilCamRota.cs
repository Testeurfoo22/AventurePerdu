using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccueilCamRota : MonoBehaviour
{
    public int[] angle = new int[2];
    public int vitesse = 5; 
    int anglePassage;

    //ce programme permet juste de faire le mouvement de rotation de l'accueil
    // Start is called before the first frame update
    void Start()
    {
        transform.localEulerAngles = new Vector3(0, 0, 0);
        anglePassage = angle[1];
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 deplacement = Vector3.right;
        //change de direction apres passages des extremums
        if (anglePassage == angle[1]){
            deplacement = Vector3.up;
        }
        else{
            deplacement = Vector3.down;
        }
        transform.Rotate(deplacement * vitesse * Time.deltaTime);

        //changement de l'angle voulu 
        if (transform.localEulerAngles.y >= angle[1]){
            anglePassage = angle[0];
        }
        else if (transform.localEulerAngles.y <= angle[0]){
            anglePassage = angle[1];
        }
    }
}
