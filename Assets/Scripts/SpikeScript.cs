using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeScript : MonoBehaviour
{

    private AudioSource playerAudio;
    public AudioClip hitSound;
    public int lifeTime;

    IEnumerator WaitThenDie()
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        playerAudio = GetComponent<AudioSource>();
        StartCoroutine(WaitThenDie());
    }

    //It's getting all the shapes and putting the score to 0 and size to 1.
     void OnCollisionEnter(Collision collision)
    {
        ContactPoint contactPoint = collision.GetContact(0);
        GameObject collidingObject = contactPoint.otherCollider.gameObject;
       
        if(collidingObject.CompareTag("Prop"))
        {
            playerAudio.PlayOneShot(hitSound, .06f);
            //Destroy(this.gameObject);
            collidingObject.transform.parent = null;

            //Debug.Log("hit");      
            //Debug.Log("SPIKE: Size - " + (collision.gameObject.GetComponent<Collider>().bounds.size.y / 5));
            //Debug.Log("SPIKE: Score - " + Mathf.CeilToInt(collision.gameObject.GetComponent<Collider>().bounds.size.y));
            GameObject.Find("Player").GetComponent<BallController>().AdjustSize(-1 * (collidingObject.GetComponent<Collider>().bounds.size.y / 5));
            GameObject.Find("Player").GetComponent<BallController>().AdjustScore(Mathf.CeilToInt (- 1 * collidingObject.gameObject.GetComponent<Collider>().bounds.size.y));
            Destroy(collidingObject);
        }
        if(collidingObject.CompareTag("Player"))
        {
            playerAudio.PlayOneShot(hitSound, .06f);
            Destroy(this.gameObject);
        }

    }
}
