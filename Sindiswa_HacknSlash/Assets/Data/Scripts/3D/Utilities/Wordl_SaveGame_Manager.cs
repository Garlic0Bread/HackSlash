using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace OWL
{
    public class Wordl_SaveGame_Manager : MonoBehaviour//handles loading into world, loading save files 
    {
        //is a singletone. allows script to be accessed anywhere at anytime 
        //singleton pattern ensures that there's only one instance of this script
        public static Wordl_SaveGame_Manager instance;
        [SerializeField] int worldSceneIndex = 1;

        public Wordl_SaveGame_Manager saveGameManager
        {
            get { return instance; }
            set { instance = value; }
        }//allow instance to be used in a team
        private void Awake()
        {
            if (instance == null)//functionality for there only being one instance in the scene
            {
                instance = this;
            }
            else
            {
                Destroy(instance);
            }
        }
        private void Start()
        {
            DontDestroyOnLoad(gameObject);//stays with us when loading into the world or mainMenu
        }
        private void Update()
        {
            if(Input.GetKeyUp(KeyCode.Escape))
            {
                Application.Quit();
            }
        }
        public IEnumerator LoadNewGame()
        {
            AsyncOperation loadOperation = SceneManager.LoadSceneAsync(worldSceneIndex);
            yield return null;
        }
        public int GetWorldSceneIndex()
        {
            return worldSceneIndex;
        }
    }
}

