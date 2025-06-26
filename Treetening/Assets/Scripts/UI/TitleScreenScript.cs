using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreenScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void playBtn()
    {
        SceneManager.LoadScene(1);
    }

    public void exitBtn()
    {
        Application.Quit();
    }
}
