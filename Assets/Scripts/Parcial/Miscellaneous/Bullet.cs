using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bullet : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.gameObject.name == "Player")
        {
            //SceneManager.LoadScene("SampleScene");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        }
        else if (collision.transform.gameObject.tag == "Decoy")
        {
            Destroy(collision.gameObject);
        }
    }
}
