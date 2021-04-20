using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecuentialButton : MonoBehaviour, ISelectable
{
    // Start is called before the first frame update
    [SerializeField] string valueButton;
    public SecuenciaEnigma secuenciaEnigma;
    public FlaskEnigma enigma;
    public PauseManager pauseManager;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ButtonUP()
    {
        var animator = GetComponent<Animator>();
        if (animator != null)
            animator.SetTrigger("isClick");

        switch (valueButton)
        {
            case "Left":
                secuenciaEnigma.ChangeSecuence(-1);
                break;
            case "Right":
                secuenciaEnigma.ChangeSecuence(1);
                break;
            case "End":
                enigma.CheckEnigma();
                break;
            case "Pause":
                if (pauseManager != null)
                    pauseManager.PauseGame();
                break;
            default:
                Debug.Log("Set button Up " + valueButton);
                secuenciaEnigma.AddSecuence(valueButton);
                break;
        }

    }

    public void OnSelected()
    {
        GetComponent<Outline>().OutlineWidth = 10;
    }

    public void Deselected()
    {
        GetComponent<Outline>().OutlineWidth = 0;
    }
}
