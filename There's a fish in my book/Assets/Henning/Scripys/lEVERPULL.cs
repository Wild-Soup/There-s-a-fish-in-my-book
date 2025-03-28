using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class lEVERPULL : MonoBehaviour
{
    [SerializeField] private SlottsMachine machine;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SPIN()
    {
        machine.StartCoroutine(machine.SpinMachine());
    }


}
