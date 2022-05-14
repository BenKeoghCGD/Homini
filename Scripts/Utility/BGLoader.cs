using UnityEngine;
using NMaterial;

public class BGLoader : MonoBehaviour
{
    private void Start()
    {
        BeginLoad();
        Destroy(this.gameObject);
    }

    /*
     * 
     *  ALL PRE-LOAD INITIALIZATIONS
     * 
     */
    void BeginLoad()
    {
    }
}