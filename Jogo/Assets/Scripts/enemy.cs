using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    private int hitCount;
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag == "mud"){

            animator.SetBool("isHit", true);

            hitCount++;
            if(hitCount >= 5) {
                Debug.Log("reached 5");
                Destroy(this.gameObject);
            }

            Destroy(other.gameObject);
            animator.SetBool("isHit", false);
        }
    }
}
