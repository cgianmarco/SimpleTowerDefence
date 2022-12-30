using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ShopManager : MonoBehaviour
{
    public static ShopManager Instance;

    

    [SerializeField] private TMP_Text scoreText;
    int score;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        SetScore(40);
    }


    void SetScore(int newScore)
    {
        score = newScore;
        scoreText.text = score.ToString();
    }

    public void IncreaseScore(int plus)
    {
        SetScore(score + plus);
    }

    public void DecreaseScore(int minus)
    {
        SetScore(score - minus);

        if (score < 0)
            SceneManager.LoadScene("LoseScreen");
    }

    public bool BuyUnit(int price)
    {
        if (price > score) return false;

        SetScore(score - price);
        return true;


    }

    public bool CanBuyTurret(int price)
    {
        return (price <= score);
    }
}
