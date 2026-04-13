using UnityEngine;
using UnityEngine.SceneManagement;

public class Door_Exit : MonoBehaviour
{
    [Header("Inscribed")]
    public string NextScene;
    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponentInParent<King_Midas>() != null)
        {
            SceneManager.LoadScene(NextScene);
        }
    }
}
