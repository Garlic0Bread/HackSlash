using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetCharacter : MonoBehaviour
{
    public CharacterSelectManager charSelMan;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OnMouseDown()
    {
        if (this.CompareTag("Player 1"))
        {
            charSelMan.Player1_Char = this.gameObject.tag;
        }
        else if (this.CompareTag("player 2"))
        {
            charSelMan.Player2_Char = this.gameObject.tag;
        }
    }

    // Update is called once per frame
    void Update()
    {
        OnMouseDown();
    }
}
