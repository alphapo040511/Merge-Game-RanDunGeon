using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    // Start is called before the first frame update
    void Start()
    {
        cardRigidbody = GetComponent<Rigidbody>();
        cardType = Random.Range(1, 5);
        changeCardType();
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

    private void OnCollisionEnter(Collision otherCard)
    {
        if (cardRank <= 2)
        {
            target = otherCard.gameObject;
            if (GetComponentInParent<CardManager>().stopDraw == true)
            {
                if (otherCard.transform.tag == "Card")
                {
                    if (cardType == otherCard.transform.GetComponent<CardState>().cardType && cardRank == otherCard.transform.GetComponent<CardState>().cardRank)
                    {
                        Debug.Log("Merge");
                        cardRank ++;
                        GetComponentInParent<CardManager>().doMerge = true;
                    }
                }
            }
        }
    }

    IEnumerator Merge()
    {
        yield return null;
        

    }
}
