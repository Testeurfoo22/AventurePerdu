using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Luciole : MonoBehaviour
{
    public float distanceTerrain;   //distance min entre le terrain et la luciole
    public float[] distancemax = new float[2];      //distance max du point initiale, sur les 3 faces

    public float RangeLightInit;    //range initiale
    public float RangeLightFinal;   //range maximale
    public GameObject cameraPlayer;

    Vector3 positionAwake;

    // Start is called before the first frame update
    void Awake()
    {
        gameObject.GetComponent<Light>().range = RangeLightInit;
        positionAwake = transform.localPosition;
        StartCoroutine(Deplacement());
    }

    // Update is called once per frame
    void Update()
    {

    }
    // check si le vector3 final est hors de la zone de deplacement, et le deplace, change aussi la range le point light
    IEnumerator Deplacement(){
        var initPos = transform.localPosition;

        float Distance = Random.Range(0F, 1F);
        float temps = Random.Range(1F, 3F);
        Vector3 direction = GestionTerrain(Random.onUnitSphere * Distance + initPos);
        if(direction.x < positionAwake.x + distancemax[0]){
            direction.x = positionAwake.x + distancemax[0];
        }
        if(direction.y < positionAwake.y + distancemax[0]){
            direction.y = positionAwake.y + distancemax[0];
        }
        if(direction.z < positionAwake.z + distancemax[0]){
            direction.z = positionAwake.z + distancemax[0];
        }
        if(direction.x > positionAwake.x + distancemax[1]){
            direction.x = positionAwake.x + distancemax[1];
        }
        if(direction.y > positionAwake.y + distancemax[1]){
            direction.y = positionAwake.y + distancemax[1];
        }
        if(direction.z > positionAwake.z + distancemax[1]){
            direction.z = positionAwake.z + distancemax[1];
        }
        
        float NextRange = Random.Range(RangeLightInit, RangeLightFinal);
        var initrange = gameObject.GetComponent<Light>().range;
        float t = 0;
        while (t <= temps){
            Vector3 ValInter = Vector3.Lerp(initPos, direction, t);
            transform.localPosition = ValInter;

            float ValRangeInter = Mathf.Lerp(initrange, NextRange, t);
            gameObject.GetComponent<Light>().range = ValRangeInter;

            t += Time.deltaTime;
            yield return null;
        }
        transform.localPosition = direction;
        gameObject.GetComponent<Light>().range = NextRange;
        yield return new WaitForSecondsRealtime(Random.Range(0F, 1F));
        yield return Deplacement();
    }
    //gestion y de la luciole pour eviter le terrain
    Vector3 GestionTerrain(Vector3 direction){
        try{
            float hauteurTerrain =
                Terrain.activeTerrain.transform.localPosition.y +
                Terrain.activeTerrain.SampleHeight(direction) +
                distanceTerrain;

            return new Vector3(
                direction.x,
                Mathf.Max(transform.localPosition.y, hauteurTerrain),
                direction.z);
        }
        catch{
            return direction;
        }
    }
}
