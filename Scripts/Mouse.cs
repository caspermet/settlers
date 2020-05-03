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
                if (hit.transform.name == "Settlement")
                {
                    Debug.Log("settlement clcik");

                    
                    var town = hit.transform.GetComponent<SettlementLocalization>();
                    town.CreateSettlement();
                    /*
                    for (int i = 0; i < nearTown.Count; i++)
                    {
                        Material material = nearTown[i].GetComponent<Renderer>().material;
                        material.color = Color.blue;
                    }*/
                    //foreach (var item in nearTown)
                    // {
                    // Debug.Log(item.transform.name);
                    //Material material = item.GetComponent<Renderer>().material;
                    //material.color = Color.blue;
                    // }
                }
                else if(hit.transform.name == "Route")
                {
                    var route = hit.transform.GetComponent<RouteLocalization>();
                    route.CreateRoute();
                    /* var route = hit.transform.GetComponent<RouteLocalization>();

                      var material1 = route.settlement1.GetComponent<Renderer>().material;
                      var material2 = route.settlement2.GetComponent<Renderer>().material;

                      material1.color = Color.green;
                      material2.color = Color.green;*/
                }

                else
                {
                    Debug.Log("This isn't a Player");
                }
            }
        }
    }
}
