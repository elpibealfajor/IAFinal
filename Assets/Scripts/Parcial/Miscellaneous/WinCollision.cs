using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinCollision : MonoBehaviour
{
    public bool isLastLevel = false;
    private void OnTriggerEnter(Collider other)
    {
        if (isLastLevel == false)
        {
            //SceneManager.LoadScene("Scene 2");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

        }
        else
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}
