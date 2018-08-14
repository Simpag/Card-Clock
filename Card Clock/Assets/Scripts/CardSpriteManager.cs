using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSpriteManager : MonoBehaviour {


    private bool offset = true;
    private float offsetAmount = 0.1f;

    private void OnEnable()
    {
        if (!offset)
            return;
        
        int cardsOnScreen = transform.parent.parent.GetComponent<CardDeck>().activeChildren;
        transform.position = new Vector3(transform.position.x, transform.position.y + offsetAmount * (cardsOnScreen/13));
    }

}
