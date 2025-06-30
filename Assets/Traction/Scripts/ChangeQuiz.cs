using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChangeQuiz : MonoBehaviour
{
    public SpriteRenderer tvSpriteRenderer;
    public Sprite homeSprite;
    public Sprite quizSprite;
    public TextMeshPro questionText;
    public string[] questions;
    private int sId;
    private float timeRemaining = 0;
    // Start is called before the first frame update
    void Start()
    {
        if (questions.Length > 0){
            sId = 0;
        } else {
            Debug.LogError("You did not put any question for the pub quiz");
        }

    }

    // Update is called once per frame
    void Update()
    {
        timeRemaining -= Time.deltaTime;
    }

    public void ChangeQuestion()
    {
        if (timeRemaining <= 0)
        {
            if (sId == 0)
            {
                tvSpriteRenderer.sprite = quizSprite;
            }
            if (sId == questions.Length)
            {
                tvSpriteRenderer.sprite = homeSprite;
                questionText.text = "";
                sId = -1;
            }
            else
            {
                questionText.text = questions[sId];
            }
            sId++;
            timeRemaining = 1.0f;
        }
    }
}
