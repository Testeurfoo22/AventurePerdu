using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class IA : MonoBehaviour
{
    public GameObject player;
    public int TempsFreeze;
    NavMeshAgent agent;
    AudioSource music;
    bool isFollow = false;
    bool enFuite = false;
    // Start is called before the first frame update
    void Awake()
    {
        isFollow = false;
        music = GetComponent<AudioSource>();
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!enFuite){
            if (Vector3.Distance(transform.position, player.transform.position) < 30 && !isFollow){
                isFollow = true;
                GetComponent<Animator>().SetTrigger("walk");
            }
            else if(Vector3.Distance(transform.position, player.transform.position) > 60 && isFollow){
                GetComponent<Animator>().SetTrigger("stop");
                isFollow = false;
            }
            if (isFollow){
                agent.destination = player.transform.position;
            }    
        }
    }
    private void OnTriggerEnter(Collider other) {
        if (other.name == "Main Camera"){
            if (! inventaire.IsEmpty()){
                GetComponent<Animator>().SetTrigger("attack");
                StartCoroutine(playermovement.Freeze(TempsFreeze));
                GameObject itemSteel = inventaire.Steal();
                music.Play();
                Vector3 fuite = (Random.onUnitSphere * Random.Range(10, 16) + transform.position);   
                fuite = new Vector3(fuite.x, transform.position.y, fuite.z); 
                StartCoroutine(Fuite(fuite, itemSteel));
            }   
        }
    }
    IEnumerator Fuite(Vector3 destination, GameObject itemObject){
        enFuite = true;
        Vector3 X = (agent.transform.position - destination);
        var initTimer = Time.time;
        while((-1 > X.x) || (X.x > 1) && (-1 > X.z) || (X.z > 1) && (Time.time - initTimer) < 3){
            agent.destination = destination;
            X = (agent.transform.position - destination);
            yield return null;
        }
        itemObject.transform.position = transform.position;
        itemObject.SetActive(true);
        GetComponent<Animator>().SetTrigger("stop");
        yield return new WaitForSecondsRealtime(4);
        GetComponent<Animator>().SetTrigger("walk");
        enFuite = false;
    }
}
