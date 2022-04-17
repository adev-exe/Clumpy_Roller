using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    // Countdown
    public float initCountdownTime = 300f;
    private float time;

    // Spawns
    public float[] sizes = new float[5];

    // Spawn Shapes
    public int spawnShapeAmount;
    public float spawnShapeCycleTime;
    public GameObject[] shapes;

    // Spawn Spikes
    public int spawnSpikeAmount;
    public float spawnSpikeCycleTime;
    public float dangerIndicateTime;
    public GameObject spike;
    public GameObject indicator;
   

    // Display Text
    public Text displayTime;
    public Text displayScore;

    // SOUND
    private AudioSource playerAudio;
    public AudioClip gameSound;

    // Game Controller
    private bool running = false;

    // public static string finalScore = 0; 

    /*
     * 
     *  HELPER FUNCTIONS
     * 
     */

    void DisplayTime(float time)
    {
        if(time >= 0)
        {
            float min = Mathf.FloorToInt(time / 60);
            float sec = Mathf.FloorToInt(time % 60);
            displayTime.text = string.Format("Time: {0:00}:{1:00}", min, sec);
        }

    }

    /*
     * 
     *  RUNNING SCRIPTS
     * 
     */
    private IEnumerator Countdown()
    {
        while (time > 0)
        {
            time -= Time.deltaTime;
            DisplayTime(time);
            yield return null;
        }

        // Stop Game Script
         playerAudio.Stop();
         
    }

    private IEnumerator DisplayScore()
    {
        int score = GameObject.Find("Player").GetComponent<BallController>().GetScore();
        while (time > 0)
        {
            score =  GameObject.Find("Player").GetComponent<BallController>().GetScore();
            displayScore.text = "Score: " + score.ToString();
            // finalScore =  score.ToString();
            yield return null;
        }            

        GameScore.FINAL_SCORE = score;
       

    }

    private IEnumerator SpawnShapes()
    {
        while (time > 0)
        {
            for(int i = 0; i <= spawnShapeAmount; i++)
            {
                int shapeIndex = UnityEngine.Random.Range(0, shapes.Length);
                int sizeIndex = UnityEngine.Random.Range(0, sizes.Length);
                Vector3 spawnLocation = new Vector3(Random.Range(-190, 191), (sizes[sizeIndex] == 1 ? 0 : sizes[sizeIndex] / 2), Random.Range(-190, 191));
                GameObject clone = Instantiate(shapes[shapeIndex], spawnLocation, Quaternion.identity);
                clone.transform.localScale *= sizes[sizeIndex];
            }
            yield return new WaitForSeconds(spawnShapeCycleTime);
        } 
    }

    private IEnumerator SpawnSpikeWithIndicator()
    {
        int shapeIndex = UnityEngine.Random.Range(0, shapes.Length);
        int sizeIndex = UnityEngine.Random.Range(0, sizes.Length - 1);
        Vector3 spawnLocation = new Vector3(Random.Range(-190, 191), (sizes[sizeIndex] == 1 ? 0 : sizes[sizeIndex] / 20), Random.Range(-190, 191));
        
        if (dangerIndicateTime > 0f)
        {
            GameObject cloneIndicator = Instantiate(indicator, spawnLocation, Quaternion.identity);
            cloneIndicator.GetComponent<Renderer>().material.color = Color.red;
            cloneIndicator.transform.localScale *= (sizes[sizeIndex] * 3);
            yield return new WaitForSeconds(dangerIndicateTime);
            Destroy(cloneIndicator);
        }

        GameObject clone = Instantiate(spike, spawnLocation, Quaternion.identity);
        clone.transform.localScale *= (sizes[sizeIndex] < 1 ? 1 : sizes[sizeIndex]);
    }

    private IEnumerator SpawnSpikes()
    {
        while (time > 0)
        {
            for (int i = 0; i <= spawnSpikeAmount; i++)
            {
                StartCoroutine(SpawnSpikeWithIndicator());
            }
            yield return new WaitForSeconds(spawnSpikeCycleTime);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        time = initCountdownTime;

        // May need to move this elsewhere when we have menus
        StartCoroutine(SpawnShapes());
        StartCoroutine(SpawnSpikes());
        StartCoroutine(Countdown());
        StartCoroutine(DisplayScore());

        // SOUND 
        playerAudio = GetComponent<AudioSource>();
        // playerAudio.PlayOneShot(gameSound, .06f);
    }

    
    // Update is called once per frame
    void Update()
    {
        if(time <= 0)
        {
            Debug.Log("Game Over");
            StopCoroutine(SpawnShapes());
            StopCoroutine(SpawnSpikes());
            StopCoroutine(Countdown());
            StopCoroutine(DisplayScore());
            SceneManagerScript.RetryScreen();
        }
    }
    
}
