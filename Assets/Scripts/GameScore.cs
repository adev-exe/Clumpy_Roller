using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class GameScore : MonoBehaviour
{

    public TMP_Text textBox;

    public static int FINAL_SCORE;

    // Start is called before the first frame update
    void Start()
    {
     //   textBox = GetComponent<TMP_Text>();
        textBox.text = "Score: " + FINAL_SCORE;
        Debug.Log(FINAL_SCORE.ToString());
    }

    // Update is called once per frame
    void Update()
    {
        // GameController.finalScore;
        // Debug.Log(GameController.finalScore);
    }
}
