using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SecuenceType
{
    Right,
    Left
}
public class ButtonBook : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject arrowFather;

    public SecuenceType secuenceType;

    private bool coroutineAllowed;

    public int pageShow;

    public GameObject[] pages;


    void Start()
    {
        pageShow = 0;
        DisableNotVisiblePages();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnMouseDown()
    {
        StartCoroutine("ChangePage");
    }

    private IEnumerator ChangePage()
    {


        if (secuenceType == SecuenceType.Left && pageShow > 0)
        {
            pageShow--;
            if (pageShow < 0) pageShow = 0;

            pages[pageShow].gameObject.SetActive(true);
            var anim = pages[pageShow].GetComponent<Animation>();
            anim.Play("book_page_left");
        }

        if (secuenceType == SecuenceType.Right && pageShow < pages.Length - 1)
        {
            UpdatePageSecuenceNumber(pageShow);
            pages[pageShow].gameObject.SetActive(true);
            var anim = pages[pageShow].GetComponent<Animation>();
            anim.Play("book_page_right");

            pageShow++;
            if (pageShow == pages.Length) pageShow--;
        }


        yield return new WaitForSeconds(0.4f);

        Debug.Log("Page Show " + pageShow);
        
        UpdatePageSecuenceNumber(pageShow);
        DisableNotVisiblePages();


    }

    private void UpdatePageSecuenceNumber(int currentPageShow)
    {
        var child = arrowFather.GetComponentsInChildren<ButtonBook>();

        if (child != null)
        {
            for (var x = 0; x < child.Length; x++)
            {
                child[x].pageShow = currentPageShow;
            }
        }
    }

    private void DisableNotVisiblePages()
    {
        if (pages.Length == 0) return;

        for (var x = 0; x < pages.Length; x++)
        {
            pages[x].SetActive(false);

            if (x == pageShow || (x == pageShow + 1 && secuenceType == SecuenceType.Right))
                pages[x].SetActive(true);

        }
    }
    private IEnumerator RotateWheel()
    {
        coroutineAllowed = false;

        for (int i = 0; i <= 11; i++)
        {
            transform.Rotate(0f, 3f, 0f);
            yield return new WaitForSeconds(0.01f);
        }

        coroutineAllowed = true;

        pageShow += 1;

        if (pageShow > 9)
        {
            pageShow = 0;
        }

        //Rotated(name, numberShown);
    }
}
