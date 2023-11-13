using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutTree : MonoBehaviour
{
    private bool canHit;

    [SerializeField]
    private int life = 3;

    [SerializeField]
    private GameObject gm;

    private void Start()
    {
        gm = GetComponentInParent<GameObject>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("RangoCharacter"))
        {
            Debug.Log("Entra");
            canHit = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("RangoCharacter"))
        {
            Debug.Log("Entra");
            canHit = false;
        }
    }

    private void Update()
    {
        
        if(canHit == true) 
        { 
            if(Input_Manager._INPUT_MANAGER.GetButtonCut())
            {
                life--;
            }
            if(life <= 0)
            {
                Debug.Log("Destroy");
                Destroy(gm);
            }
        }
    }



}