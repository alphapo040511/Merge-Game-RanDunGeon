using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public GameObject cardPrefab;
    private int isCard;
    public bool cardDrawing = false;
    public bool doMerge = false;
    public bool stopDraw = false;

    // Start is called before the first frame update
    void Start()
    {
        cardDrawing = false;
    }

    // Update is called once per frame
    void Update()
    {
        isCard = transform.childCount;

        if(isCard < 5 && cardDrawing == false && doMerge == false)
        {
            stopDraw = false;
            cardDrawing = true;
            StartCoroutine(AddCard());
        }
        if(isCard == 5)
        {
            stopDraw = true;
        }
    }


    private IEnumerator AddCard()
    {
        float addCardCoolTime;
 
        Instantiate(cardPrefab, transform);
        
        addCardCoolTime = 0.6f;
        while(addCardCoolTime >=0)
        {
            addCardCoolTime -= Time.deltaTime;
            yield return null;
        }

        cardDrawing = false;
    }
}
