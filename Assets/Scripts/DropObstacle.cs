using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropObstacle : MonoBehaviour
{
    [SerializeField] GameObject obstacle;
    GameObject[] agents;
    // Start is called before the first frame update
    void Start()
    {
        agents = GameObject.FindGameObjectsWithTag("Agent");
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0)) {
            RaycastHit hitInfo;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if(Physics.Raycast(ray.origin,ray.direction, out hitInfo)){
                Instantiate(obstacle, hitInfo.point, hitInfo.transform.rotation);
                foreach( GameObject a in agents){
                    a.GetComponent<AIControl>().DetectNewObstacle(hitInfo.point);
                }
            }
        }
        
    }
}
