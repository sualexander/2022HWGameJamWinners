using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    [SerializeField] Text dialogueBox;
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

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayBugleSlide()
    {
        anim.Play("bugleslide");
        Debug.Log("Bugle Slide");
    }

    public void SetDialog(string msg)
    {
        dialogueBox.text = msg;
    }
}
