using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class End_Game : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject.Find("Canvas").transform.Find("Game_Over").gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // si la vie arrive à 0, on le canvas de fin de jeu
        if (GlobalVariables.grangeCurrentHealth == 0)
        {
            GameObject.Find("Canvas").transform.Find("Game_Over").gameObject.SetActive(true);
            //stop the game
            Time.timeScale = 0;
        }
    }
}
