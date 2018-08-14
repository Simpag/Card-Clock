using UnityEngine;

public class Card : MonoBehaviour {

    public enum CardColors
    {
        Black,
        Red
    }

    public enum CardTypes
    {
        Clubs,
        Diamond,
        Hearts,
        Spades
    }

    private void OnDisable()
    {
        transform.parent.GetComponent<CardDeck>().activeChildren--;
    }

    private void OnEnable()
    {
        transform.parent.GetComponent<CardDeck>().activeChildren++;
    }

    [SerializeField]
    private int cardNumber;
    [SerializeField]
    private CardTypes cardType;
    [SerializeField]
    private Sprite cardSprite;

    public int CardNumber { get { return cardNumber; } }
    public CardTypes CardType {get { return cardType; } }
    public Sprite CardSprite { get { return cardSprite; } }
}
