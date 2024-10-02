using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class GetCharacter : MonoBehaviour
{
    public CharacterSelectManager charSelMan;
    public GameObject character;

    // Start is called before the first frame update
    void Start()
    {
        //character.SetActive(false);
    }

    public void HasBeenSelected()
    {
        if (charSelMan.Player1Turn == true)
        {
            charSelMan.Player1_Char = this.gameObject.tag;
            Debug.Log("tag is " + charSelMan.Player1_Char);
        }
        if (charSelMan.Player2Turn == true)
        {
            charSelMan.Player2_Char = this.gameObject.tag;
            Debug.Log("tag is " + charSelMan.Player2_Char);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
