using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class King_Midas : MonoBehaviour
{
    [Header("Inscribed")]
    public float speed = 30;
    public GameObject leftArm;
    public GameObject rightArm;
    public Material goldMaterial;

    private bool glovedUp = false;

    // Update is called once per frame
    void Update()
    {
        float xAxis = Input.GetAxis("Horizontal");
        float zAxis = Input.GetAxis("Vertical");

        Vector3 pos = transform.position;
        pos.x += xAxis * speed * Time.deltaTime;
        pos.z += zAxis * speed * Time.deltaTime;
        transform.position = pos;

        if (Input.GetKeyDown(KeyCode.G))
        {
            switchGlove();
        }
    }

    void switchGlove()
    {
        glovedUp = !glovedUp;
        ((Behaviour)leftArm.GetComponent("Halo")).enabled = glovedUp;
        ((Behaviour)rightArm.GetComponent("Halo")).enabled = glovedUp;
    }

    void OnTriggerStay(Collider other)
    {
        string[] ignoreNames = { "Floor", "Door_Entrance", "Door_Exit"};
        if (!glovedUp) return;
        if (ignoreNames.Contains(other.gameObject.name)) return;
        MeshRenderer otherRenderer = other.GetComponentInParent<MeshRenderer>();
        if (otherRenderer.sharedMaterial == goldMaterial) return;
        otherRenderer.material = goldMaterial;
        Rigidbody rb = other.GetComponentInParent<Rigidbody>();
        if (rb != null)
        {
            other.GetComponentInParent<Rigidbody>().mass = 1000000;
        }
    }
}
