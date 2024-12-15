using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostWwiseEvent : MonoBehaviour
{
    public AK.Wwise.Event triggerSteps;
    // Start is called before the first frame update
    void Start()
    {
        triggerSteps.Post(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
