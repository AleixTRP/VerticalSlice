using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventAxeHit : MonoBehaviour
{
    public void AxeHit()
    {
        Audio_Manager.instance.Play("AxeHit", transform.position);
    }
}
