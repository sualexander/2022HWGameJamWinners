using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    [SerializeField] Text dialogBox;
    [SerializeField] GameObject panel;
    string toType = "";
    Animator anim;

    Animator health;
    
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
        health = transform.Find("HealthBar").GetComponent<Animator>();
    }

    public void SetHealth(int health)
    {
        if (this.health != null) this.health.SetInteger("Health", health);
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

    public void StartOver()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
