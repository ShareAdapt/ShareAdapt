using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseController : MonoBehaviour
{
    public GameObject target;

    public void OnClick_CloseTarget()
    {
        target.SetActive(false);
    }
}
