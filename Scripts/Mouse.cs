using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Mouse : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(ray, out hit))
            {
                if(hit.transform.name == "Route")
                {
                    var route = hit.transform.GetComponent<RouteLocalization>();
                    route.CreateRoute();
                }
            }
        }
    }
}
