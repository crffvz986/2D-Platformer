using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : MonoBehaviour
{
    public Transform firePoint;
    public GameObject arrowPrefab;

	void Update ()
    {
		if(Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
	}

    void Shoot ()
    {
        Instantiate(arrowPrefab, firePoint.position, firePoint.rotation);
    }
}
