using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Horloge : MonoBehaviour
{
    public float time;  //temps de pose
    public float executiontime; //temps d,interpolation
    public Vector3 distanceVecteur = new Vector3(0F, 0F, 0F);
    // Start is called before the first frame update
    void Awake()
    {
        StartCoroutine(Rotate(transform.localEulerAngles, distanceVecteur, time));   
    }

    // Update is called once per frame
    void Update()
    {

    }
    //fait tourner nos fleches, sur des angles donne et sur un temps donne
    IEnumerator Rotate(Vector3 Depart, Vector3 Final, float time){
        yield return new WaitForSecondsRealtime(time); 
        float t = 0;
        while(t <= executiontime){
            Vector3 ValInter = Vector3.Lerp(Depart, Depart + Final, t);
            transform.localEulerAngles = ValInter;
            t += Time.deltaTime;
            yield return null;
        }
        transform.localEulerAngles = Depart + Final;
        yield return (Rotate(transform.localEulerAngles, -Final, time));
    }
}
