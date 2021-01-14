using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PrevSangre : MonoBehaviour
{
    public Image imSangre;
    IEnumerator Start()
    {
        yield return new WaitForSeconds(0.1f);
        Vida m = GetComponentInParent<Vida>();
        if (m!=null && m.imSangre == null)
        {
            m.imSangre = imSangre;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
