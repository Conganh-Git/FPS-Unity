using System.Collections;
using UnityEngine;

public class Footsteps : MonoBehaviour
{

    [SerializeField] private AudioSource f1;
    [SerializeField] private AudioSource f2;
    [SerializeField] private AudioSource f3;
    [SerializeField] private AudioSource f4;
    [SerializeField] bool isStepping;
    [SerializeField] int soundNumber;
    
    private PlayerController playerController;

    private void Start()
    {
        playerController = GetComponent<PlayerController>();
    }

    void Update()
    {
        if (playerController.MoveInput != Vector2.zero) 
        {
            if (!isStepping)
            {
                isStepping = true;
                soundNumber = Random.Range(1, 5);
                StartCoroutine(Footstep());
            }

        }    
    }

    IEnumerator Footstep()
    {
        if(soundNumber == 1)
        {
            f1.Play();
        }
        if (soundNumber == 2)
        {
            f2.Play();
        }
        if (soundNumber == 3)
        {
            f3.Play();
        }
        if (soundNumber == 4)
        {
            f4.Play();
        }
        
        if (playerController.IsSprinting)
        {
            yield return new WaitForSeconds(0.3f);
        }
        else
        {
            yield return new WaitForSeconds(0.6f);
            
        }    
        isStepping = false;   
    }
}
