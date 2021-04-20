using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    // Start is called before the first frame update

    bool isPause;
    public Canvas canvasGame;
    public Canvas canvasPause;
    public playerFps player;

    void Start()
    {
        canvasGame.gameObject.SetActive(true);
        canvasPause.gameObject.SetActive(false);

    }

    // Update is called once per frame
    public void PauseGame()
    {
        Debug.Log("PauseGame");
        Time.timeScale = 0;
        isPause = false;
        canvasPause.gameObject.SetActive(true);
        canvasGame.gameObject.SetActive(false);
        player.canMove = false;
        player.gameObject.GetComponent<DragAndDrop_3D>().enabled = false;
    }

    public void ContinueGame()
    {
        Debug.Log("ContinueGame");
        player.gameObject.GetComponent<DragAndDrop_3D>().enabled = true;

        Time.timeScale = 1;
        isPause = true;
        canvasPause.gameObject.SetActive(false);
        canvasGame.gameObject.SetActive(true);
        player.canMove = true;

    }
}
