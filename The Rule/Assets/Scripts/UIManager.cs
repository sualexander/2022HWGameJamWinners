using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    [SerializeField] Text dialogBox;
    [SerializeField] GameObject panel;
    string toType = "";
    Animator anim;

    Animator health;
    Text coins;
    TextMeshProUGUI text;
    Animator alert;
    
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
        coins = transform.Find("MoneyCount").GetComponent<Text>();
        if (transform.Find("Alert"))
        {
            alert = transform.Find("Alert").GetComponent<Animator>();
        }
        if (transform.Find("LAWTEXT"))
        {
            text = transform.Find("LAWTEXT").GetComponent<TextMeshProUGUI>();
        }
    }

    public void SetHealth(int health)
    {
        if (this.health != null) this.health.SetInteger("Health", health);
    }

    public void SetGold(int coins)
    {
        if (this.coins != null) this.coins.text = coins.ToString();
    }

    public void PlayBugleSlide(string law)
    {
        if (text)
        {
            text.text = "THE LAW: " + law;
        }
        anim.Play("bugleslide");
    }

    public void SetDialog(string msg)
    {
        StopAllCoroutines();
        dialogBox.text = "";
        toType = msg;
        StartCoroutine(Typewriter());
    }

    public void Alert()
    {
        alert.SetTrigger("Alert");
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
