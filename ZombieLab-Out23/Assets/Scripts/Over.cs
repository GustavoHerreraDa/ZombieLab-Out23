using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Over : MonoBehaviour
{

	public GameObject CanvasNuevo;
	public GameObject CanvasGeneral;
	public GameObject Botonquecambia;
	public GameObject otrahoja;

	public Color defaultcolor;
	public Color ColorOver;
	public Color newcolorboton;
	public Renderer render;
	public Renderer renderbtnquecambia;
	public Renderer renderotrahoja;

	public GameObject exit;
	public string Web;

    private void OnMouseOver()
    {
        // Destroy the gameObject after clicking on it
        render = GetComponent<MeshRenderer>();
    	render.material.EnableKeyword("_EMISSION");
    	DynamicGI.UpdateEnvironment();
        render.material.color = ColorOver;
        render.material.SetColor("_EmissionColor",ColorOver);

        renderotrahoja = otrahoja.GetComponent<MeshRenderer>();
    	renderotrahoja.material.EnableKeyword("_EMISSION");
    	DynamicGI.UpdateEnvironment();
    	render.material.color = ColorOver;
    	renderotrahoja.material.SetColor("_EmissionColor",ColorOver);

    }
    private void OnMouseDown()
    {
        // Destroy the gameObject after clicking on it

        CanvasNuevo.SetActive(true);
        CanvasGeneral.SetActive(false);        
    }
    private void OnMouseExit()
    {
    	render = GetComponent<MeshRenderer>();
    	render.material.color = defaultcolor;

    	renderotrahoja.material.DisableKeyword("_EMISSION");
    	render.material.DisableKeyword("_EMISSION");
    }

    public void OnAceptarcliked()
    {
    	renderbtnquecambia = Botonquecambia.GetComponent<MeshRenderer>();
    	renderbtnquecambia.material.EnableKeyword("_EMISSION");
    	DynamicGI.UpdateEnvironment();
    	renderbtnquecambia.material.color =  newcolorboton;
    	renderbtnquecambia.material.SetColor("_EmissionColor", Color.red);
    	CanvasNuevo.SetActive(false);
        CanvasGeneral.SetActive(true);
    }

    public void OnexitClicked()
    {
        CanvasNuevo.SetActive(false);
        CanvasGeneral.SetActive(true);
    }

    public void OnWebcliked()
    {
    	Application.OpenURL(Web);
    }
}
