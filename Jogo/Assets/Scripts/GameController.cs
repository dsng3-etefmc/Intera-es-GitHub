using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{

    public GameObject gameOver;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update(){
        
        if (Input.GetKeyDown("r")) {
            SceneManager.LoadScene("lvl_1");
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.layer == 15)
        {
            gameOver.SetActive(true);
        }
        Destroy(collider.gameObject);
    }
}
