using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventSteps : MonoBehaviour
{
    // Start is called before the first frame update
    public void RunSteps()
    {
        Audio_Manager.instance.Play("RunSteps");
    }
}
