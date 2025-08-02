using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class nextScene : MonoBehaviour
{
    public float timer;
    public Text deathText;
    // Start is called before the first frame update
    void Start()
    {
        timer = 0f;
        
    }

    // Update is called once per frame
    void Update()
    {
            if (deathText.enabled == true)
            {
                timer += Time.fixedDeltaTime;
                if (timer > 20)
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                }
            }
    }
}
