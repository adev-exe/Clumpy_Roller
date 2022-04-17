using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeScript : MonoBehaviour
{
    void Update()
    {
        if (this.gameObject.transform.parent == null)
        {

            if (this.gameObject.GetComponent<Collider>().bounds.size.y <= GameObject.Find("Player").GetComponent<BallController>().GetSize())
            {
                this.gameObject.GetComponent<Renderer>().material.color = Color.green;
            }
            else
            {
                this.gameObject.GetComponent<Renderer>().material.color = Color.yellow;
            }
        }
        else
        {
            this.gameObject.GetComponent<Renderer>().material.color = Color.cyan;
        }
    }
}
