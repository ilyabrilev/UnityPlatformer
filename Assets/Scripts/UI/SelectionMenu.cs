using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//Common behaviour for hte menu with a list of options and an arrow
public class SelectionMenu : MonoBehaviour
{
    [Header("Options and arrows")]
    [SerializeField] private RectTransform[] options;
    [SerializeField] private RectTransform arrow;
    [SerializeField] private float arrowLeftMargin;
    [SerializeField] private float optionWidthDivider;

    [Header("Sounds")]
    [SerializeField] private AudioClip changeSound;
    private int currentPosition;

    private void Start()
    {
        currentPosition = 0;
        ChangePosition(0);
    }

    private void Update()
    {
        //Change position
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            ChangePosition(-1);
        } else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            ChangePosition(1);
        }

        //interact
        if (Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.E))
        {
            Interact();
        }
    }

    private void ChangePosition(int _change)
    {
        int newPosition = currentPosition + _change;

        if (newPosition < 0)
        {
            newPosition = options.Length - 1;
        } 
        else if (newPosition > options.Length - 1)
        {
            newPosition = 0;
        }
        ChangePositionById(newPosition);
    }

    public void ChangePositionById(int id)
    {
        if (id != currentPosition)
        {
            SFXManager.instance.PlaySound(changeSound);
        }
        currentPosition = id;
        float rectWidth = options[id].sizeDelta.x;
        float xPosition = options[id].position.x - rectWidth / optionWidthDivider - arrowLeftMargin;
        arrow.position = new Vector2(xPosition, options[id].position.y);
    }

    public void Interact()
    {
        //Access the button, call fn
        options[currentPosition].GetComponent<Button>().onClick.Invoke();
    }
}
