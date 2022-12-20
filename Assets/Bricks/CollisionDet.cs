using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDet : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name.Contains("brick"))
            Debug.Log("da");
    }
}
