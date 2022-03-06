using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    [SerializeField] Text dialogBox;
    [SerializeField] GameObject panel;
    string toType = "";
    Animator anim;

    // Start is called before the first frame update
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
        
    }
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void PlayBugleSlide()
    {
        anim.Play("bugleslide");
        Debug.Log("Bugle Slide");
    }

    public void SetDialog(string msg)
    {
        StopAllCoroutines();
        dialogBox.text = "";
        toType = msg;
        StartCoroutine(Typewriter());
    }

    IEnumerator Typewriter()
    {
        foreach (char c in toType) 
        {
            dialogBox.text += c;
            yield return new WaitForSeconds(.1f);
        }
    }

    public void FadeOut()
    {
        panel.GetComponent<Animator>().Play("FadeOut");
    }

    public void FadeIn()
    {
        panel.GetComponent<Animator>().Play("FadeIn");
    }
}
