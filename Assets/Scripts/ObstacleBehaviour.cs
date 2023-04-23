using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleBehaviour : MonoBehaviour
{

    [SerializeField] private float killTime;
    [SerializeField] private float deadTime;
    Animator anim;
    string dead = "ded";
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        StartCoroutine("KillObject");
    }

    private IEnumerator KillObject(){
        
        yield return new WaitForSeconds(killTime);
        anim.SetTrigger(dead);
                StartCoroutine("ObjectDead");


    }

    private IEnumerator ObjectDead(){
        yield return new WaitForSeconds(deadTime);
        Destroy(gameObject);
    }
}
