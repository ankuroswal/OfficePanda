// ---------------------------------------------------------------------------------------------------------------------
// - Confidential Information                                                                                          
// - Copyright 20#YEARSHORT#, Obsidian Entertainment, Inc.                                                             
// - All rights reserved.  
// - Created by: #AUTHOR# on #DATE#                                                                                    
// ---------------------------------------------------------------------------------------------------------------------

using UnityEngine;
using System.Collections;

public class FillTable : MonoBehaviour 
{
    public GameObject Root;

    private const float m_thrust = 800;
    private float m_rotate;
    private bool m_startRotate;
    private Rigidbody m_rb;

    private void Flip()
    {
        foreach (Transform child in Root.transform)
        {
            if (child.gameObject.GetComponent<Rigidbody>() == null)
            {
                Rigidbody rg = child.gameObject.AddComponent<Rigidbody>();
                rg.mass = .000001f;
            }
        }

        if (gameObject.GetComponent<Rigidbody>() == null)
        {
            m_rb = gameObject.AddComponent<Rigidbody>();

            if (m_rb != null)
            {
                m_rb.AddForce((transform.right + new Vector3(Random.Range(2, 5), Random.Range(2, 5), 0) * 50), ForceMode.Impulse);
                m_startRotate = true;
            }
        }
    }
        
    void Update()
    {
        if (Input.GetKey(KeyCode.F))
        {
            Flip();
        }

        if (m_startRotate && m_rb)
        {
            m_rotate += Time.deltaTime;
            m_rb.rotation = Quaternion.Lerp(Quaternion.identity, Quaternion.Euler(60, 0, 0), m_rotate);
//            m_rb.AddForce((transform.forward) * 10, ForceMode.Impulse);
            if (m_rotate >= 1)
            {
                m_startRotate = false;

            }
         
        }
    }

}
