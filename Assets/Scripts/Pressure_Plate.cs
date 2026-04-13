using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class Pressure_Plate : MonoBehaviour
{
    [Header("Inscribed")]
    public GameObject[] affectedGameObjects;

    void OnTriggerStay(Collider other)
    {
        string[] ignoreNames = { "Floor", "Door_Entrance", "Door_Exit", "King_Midas"};
        if (ignoreNames.Contains(other.gameObject.name)) return;
        if (other.GetComponentInParent<Rigidbody>().mass == 1000000)
        {
            this.GetComponent<BoxCollider>().isTrigger = false;
            foreach (GameObject go in affectedGameObjects)
            {
                float rotation = go.GetComponent<Double_Doors>().isOpen ? 0 : 90;
                Transform hinge = go.transform.GetChild(0);
                if (!IsGold(hinge))
                {
                    go.transform.GetChild(0).localRotation = Quaternion.Euler(0, rotation, 0);
                }
                hinge = go.transform.GetChild(1);
                if (!IsGold(hinge))
                {
                    go.transform.GetChild(1).localRotation = Quaternion.Euler(0, -rotation, 0);
                }
            }
        }
    }

    bool IsGold(Transform hinge)
    {
        MeshRenderer renderer = hinge.GetComponentInChildren<MeshRenderer>();
        if (renderer.sharedMaterial.name == "Gold_Mat")
        {
            return true;
        }
        return false;
    }
}
