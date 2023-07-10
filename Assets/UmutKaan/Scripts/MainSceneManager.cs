using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainSceneManager : MonoBehaviour
{
    // Start is called before the first frame update
    IEnumerator Start()
    {
        float textCooldown = 10f;
        yield return new WaitForSeconds(textCooldown);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
