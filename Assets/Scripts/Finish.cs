using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            GameManager.Instance.Win();           
            StartCoroutine(other.GetComponent<PlayerController>().Deceleration());
        }
    }
}
