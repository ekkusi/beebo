using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WithInitializedCheck : MonoBehaviour
{
    protected bool isInitialized = false;
    // Start is called before the first frame update
    public virtual void Start()
    {
        isInitialized = true;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
