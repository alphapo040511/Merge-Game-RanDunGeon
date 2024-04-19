using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using TreeEditor;
using UnityEngine.UIElements;
using DG.Tweening;

public class CardState : MonoBehaviour
{
    public Material attack;
    public Material defense;
    public Material recovery;
    public Material stone;

    public float moveSpeed;
    GameObject target1;
    GameObject target2;

    public bool DoMerge = false;
    private bool DoMove = false;

    public int cardType;        //1 = 공격 2 = 방어 3 = 회복 4 = 짱돌
    public int cardRank = 1;

    public TextMeshPro rankText;

    RaycastHit stopTarget;
    RaycastHit hit;

    // Start is called before the first frame update
    void Start()
    {
        cardType = Random.Range(1, 11);
        changeCardType();
        rankText.text = cardRank.ToString();
    }

    // Update is called once per frame
    void Update()
    {

        

        if (DoMove == false)
        {
            if (Physics.Raycast(transform.position, new Vector3(-1, 0, 0), out hit, 4))            //합쳐질 카드 인식
            {
                target2 = hit.collider.gameObject;
                if (target2.transform.tag == "Card" && cardRank <= 2)
                {
                    if (target2.GetComponent<CardState>().cardType == cardType && target2.GetComponent<CardState>().cardRank == cardRank)
                    {
                        MergeCard();
                    }
                }
            }
            else
            {
                if (Physics.Raycast(transform.position, new Vector3(-1, 0, 0), out stopTarget, 40))        //카드 정렬
                {
                    DoMove = true;
                    target1 = stopTarget.collider.gameObject;
                    CardMove();
                }
            }
        }
        

        
    }

    private void changeCardType()
    {
        if (cardType == 1 || cardType == 2 || cardType == 3)
        {
            cardType = 1;
            gameObject.GetComponent<MeshRenderer>().material = attack;
        }
        else if (cardType == 4 || cardType == 5 || cardType == 6)
        {
            cardType = 4;
            gameObject.GetComponent<MeshRenderer>().material = defense;
        }
        else if (cardType == 7 || cardType == 8 || cardType == 9)
        {
            cardType = 7;
            gameObject.GetComponent<MeshRenderer>().material = recovery;
        }
        else if (cardType == 10)
        {
            cardType = 10;
            gameObject.GetComponent<MeshRenderer>().material = stone;
        }
    }

    private void CardMove()
    {
        float stopPositionX = target1.transform.position.x + 4f;
        transform.DOMoveX(stopPositionX, 0.3f).OnComplete(DoMoveTurn);
    }

    void MergeCard()
    {
        GetComponent<Collider>().isTrigger = true;
        transform.Translate(new Vector3 (0,0,0.1f));
        if (DoMerge == false)
        {
            DoMerge = true;
            target2.GetComponent<CardState>().DoMerge = true;
            transform.DOMoveX(target2.transform.position.x, 0.3f).OnComplete(DoMergeTurn);

        }
    }

    public void RankUp()
    {
        DoMerge = false;
        cardRank += 1;
            rankText.text = cardRank.ToString();
    }

   void DoMoveTurn()
    {
        DoMove = false;
    }

    void DoMergeTurn()
    {
        target2.GetComponent<CardState>().RankUp();
        DoMerge = false;
        Destroy(gameObject);
    }
}
