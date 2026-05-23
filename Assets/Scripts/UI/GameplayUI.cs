using UnityEngine;
using UnityEngine.UI;

public class GameplayUI : MonoBehaviour
{
    public static GameplayUI Instance { get; private set; }

    [SerializeField] private GameObject _gameOverMenu;
    [SerializeField] private GameObject _gameOverBackground;
    [SerializeField] private GameObject _displayedHealth;
    [SerializeField] private GameObject _displayedCargo;

    [Header("Health Sprites")]
    [SerializeField] private Sprite _heartFull;
    [SerializeField] private Sprite _heartEmpty;

    [Header("Cargo Sprites")]
    [SerializeField] private Sprite _booksSprite;
    [SerializeField] private Sprite _bottlesSprite;
    [SerializeField] private Sprite _fruitSprite;
    [SerializeField] private Sprite _gooseSprite;
    [SerializeField] private Sprite _ratSprite;
    [SerializeField] private Sprite _uraniumSprite;

    private GameObject _heart1;
    private GameObject _heart2;
    private GameObject _heart3;

    private void Awake()
    {
        if (Instance != this)
        {
            if (Instance == null) { Instance = this; }
            else { Destroy(this); }
        }

        _gameOverMenu.SetActive(false);
        _gameOverBackground.SetActive(false);

        _heart1 = _displayedHealth.transform.Find("FullHeart1").gameObject;
        _heart2 = _displayedHealth.transform.Find("FullHeart2").gameObject;
        _heart3 = _displayedHealth.transform.Find("FullHeart3").gameObject;
        
        _displayedCargo.GetComponent<Image>().sprite = null;
    }

    public void DamageTaken(int currentHealth)  //Change displayed 
    {
        Debug.Log($"GameplayUI has recieved player health: {currentHealth}");
        switch (currentHealth)
        {
            case 2:
                _heart3.GetComponent<Image>().sprite = _heartEmpty;
                break;
            case 1:
                _heart2.GetComponent<Image>().sprite = _heartEmpty;
                break;
            case 0:
                _heart1.GetComponent<Image>().sprite = _heartEmpty;
                break;
        }
    }

    public void DisplayCargo(Cargo heldCargo)   //Called from PlayerStats to show current cargo
    {
        Sprite spriteRef = _displayedCargo.GetComponent<Image>().sprite;
        switch (heldCargo.name)
        {
            case "Books":
                spriteRef = _booksSprite;
                break;
            case "Bottles":
                spriteRef = _bottlesSprite;
                break;
            case "Fruit":
                spriteRef = _fruitSprite;
                break;
            case "Goose":
                spriteRef = _gooseSprite;
                break;
            case "Rat":
                spriteRef = _ratSprite;
                break;
            case "Uranium":
                spriteRef = _uraniumSprite;
                break;
            case null:
                spriteRef = null;
                break;
        }
    }

    public void GameOver()
    {
        GameStateManager.Instance.ChangeState(GameStateManager.Instance.GameOver);
        GameStateManager.Instance.SetPause();

        _gameOverMenu.SetActive(true);
        _gameOverBackground.SetActive(true);
    }
}
