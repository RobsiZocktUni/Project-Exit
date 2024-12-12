using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class GetMainLightDirection : MonoBehaviour
{
   [SerializeField] private Material SkyBoxMaterial;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SkyBoxMaterial.SetVector(name = "_MainLightDirection", transform.forward);
    }
}
