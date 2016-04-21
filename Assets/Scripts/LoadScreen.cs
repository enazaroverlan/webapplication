using UnityEngine;
using System.Collections;

public class LoadScreen : MonoBehaviour 
{
    public Texture splashImage;
    public string sceneName;
    public AsyncOperation async;

    public bool isDone = false;

    public void OnGUI()
    {
        GUI.DrawTexture(new Rect((Screen.width / 2 - (splashImage.width / 2)), (Screen.height / 2 - (splashImage.height / 2)), splashImage.width, splashImage.height), splashImage);

        if (isDone)
        {
            GUI.Label(new Rect(Screen.width / 2 - 125, Screen.height - 100, 250, 30), "Loading done! Press <color=green>[Space]</color> to contune");
        }
        else
        {
            GUI.Label(new Rect(Screen.width / 2 - 50, Screen.height - 100, 100, 30), "Loading...");
        }
    }

    public void Start()
    {
        StartCoroutine(LoadScene());
    }

    public void Update()
    {
        if (isDone)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Application.LoadLevel(sceneName);
            }
        }
    }

    public IEnumerator LoadScene()
    {
        isDone = false;
        yield return new WaitForSeconds(5);
        isDone = true;
    }
}
