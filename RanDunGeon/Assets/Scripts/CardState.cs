using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using TreeEditor;
using UnityEngine.UIElements;

public class CardState : MonoBehaviour
{
    public Material attack;
    public Material buff;
    public Material debuff;
    public Material recovery;
    public float moveSpeed;
    private bool Drawing;
    Rigidbody cardRigidbody;
    public int cardType;        //1 = 공격 2 = 디버프 3 = 회복
    public int cardRank = 1;
    private GameObject target;
    public TextMeshPro rankText;
    private float CardX, otherCardX;

    // Start is called before the first frame update
    void Start()
    {
        cardRigidbody = GetComponent<Rigidbody>();
        cardType = Random.Range(1, 5);
        changeCardType();
        rankText.text = cardRank.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        cardRigidbody.AddForce(Vector3.left * moveSpeed * Time.deltaTime);
    }

    private void changeCardType()
    {
        if (cardType == 1)
        {
            gameObject.GetComponent<MeshRenderer>().material = attack;
        }
        else if (cardType == 2)
        {
            gameObject.GetComponent<MeshRenderer>().material = buff;
        }
        else if (cardType == 3)
        {
            gameObject.GetComponent<MeshRenderer>().material = debuff;
        }
        else if (cardType == 4)
        {
            gameObject.GetComponent<MeshRenderer>().material = recovery;
        }
    }

    private void OnCollisionStay(Collision otherCard)
    {
        if (cardRank <= 2)
        {
            //if (GetComponentInParent<CardManager>().stopDraw == true)
                target = otherCard.gameObject;
            {
                if (otherCard.transform.tag == "Card")
                {
                    if (cardType == otherCard.transform.GetComponent<CardState>().cardType && cardRank == otherCard.transform.GetComponent<CardState>().cardRank)
                    {
                        GetComponentInParent<CardManager>().doMerge = true;
                        
                        CardX = transform.position.x;
                        otherCardX = otherCard.transform.position.x;
                        if (CardX > otherCardX)
                        {
                            GetComponent<BoxCollider>().isTrigger = true;
                            StartCoroutine(MergeMove());
                            
                        }
                        if (CardX < otherCardX)
                        {
                            Invoke("RankUp", 0.5f);
                        }
                    }
                }
            }
        }
    }
    void RankUp()
    {
        cardRank++;
        rankText.text = cardRank.ToString();
        GetComponentInParent<CardManager>().doMerge = false;
    }

    IEnumerator MergeMove()
    {
        transform.Translate(new Vector3(0, 0, 0.1f));
        float time = 0;
        while (time <= 0.2f)
        {
            time += Time.deltaTime;
            
            if (CardX <= otherCardX)
            {
                transform.Translate(Vector3.left * 5 * Time.deltaTime);
            }
            yield return null;
        }
        Destroy(gameObject);
        yield break;
    }
}
