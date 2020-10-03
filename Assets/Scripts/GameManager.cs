using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class GameManager : MonoBehaviour
{
    public Obstacle[] obstacles; 

    // Start is called before the first frame update
    void Start()
    {
        obstacles = FindObjectsOfType<Obstacle>();

        foreach (Obstacle obstacle in obstacles) {
            Debug.Log("Setting Obstacle: " + obstacle.gameObject.name);
            obstacle.tag = "Obstacle";
            obstacle.gameObject.layer = LayerMask.NameToLayer("Obstacle");
        } 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Restart() { 
        Scene level = SceneManager.GetActiveScene();
        SceneManager.LoadScene(level.buildIndex);
    }
}
