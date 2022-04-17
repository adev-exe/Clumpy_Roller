using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{

    public Rigidbody rigidbody;
    public float rollSpeed;
    public Transform cameraTransform;

    private GameObject player;

    private float size = 1f;
    private int score = 0;

    private AudioSource playerAudio;
    public AudioClip attachSound;
    public AudioClip spikeSound;


    public int GetScore()
    {
        return score;
    }

    public float GetSize()
    {
        return size;
    }

    public void AdjustSize(float adjustment)
    {
        // If the size adjustment is a deduction
        if (adjustment < 0)
        {
            size += (size <= 1f ? 0f : adjustment);
            if(size < 1f)
            {
                size = 1f;
            }
        }
        else
            size += adjustment;
    }

    public void AdjustScore(int adjustment)
    {
        // If the score adjustment is a deduction
        if (adjustment <= 0)
        {
            score += (score <= 0 ? 0 : adjustment);
            if (score < 0)
            {
                score = 0;
            }
        }
        else
            score += adjustment;
    }

    // Start is called before the first frame update
    void Start()
    {
      player = GameObject.Find("Player");
      playerAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 input = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
//      Vector3 movement = (input.z * cameraTransform.forward) + (input.x * cameraTransform.right);
        rigidbody.AddForce(input * rollSpeed * Time.fixedDeltaTime * (1 + ((size == 1 ? 0 : (size)/25))));
        Debug.Log("Size: " + GetSize());
        Debug.Log("Score: " + GetScore());

        
       if (Input.GetKey("escape"))

       { 
           Debug.Log("exited game");
           Application.Quit();
       }
    }

    void OnCollisionEnter(Collision collision)
    {
        // sticks object to player if player is bigger than the object
        if(collision.gameObject.CompareTag("Prop") && collision.gameObject.GetComponent<Collider>().bounds.size.y <= size)
        {
            playerAudio.PlayOneShot(attachSound, 2.0f);

            //Debug.Log("PICKUP: Size + " + (collision.gameObject.GetComponent<Collider>().bounds.size.y / 5));
            //Debug.Log("PICKUP: Score + " + Mathf.CeilToInt(collision.gameObject.GetComponent<Collider>().bounds.size.y));
            size += (collision.gameObject.GetComponent<Collider>().bounds.size.y / 5);
            score += Mathf.CeilToInt(collision.gameObject.GetComponent<Collider>().bounds.size.y);
            collision.transform.parent = this.transform;
        }

        if(collision.gameObject.CompareTag("Spikes"))
        {
            playerAudio.PlayOneShot(spikeSound, 1.0f);
        }
    }
}
