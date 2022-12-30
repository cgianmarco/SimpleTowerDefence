
public class Enemy : Unit
{

    

    private void OnEnable()
    {
        GetComponent<LifeManager>().OnKilled += unit => ShopManager.Instance.IncreaseScore(10);
        GetComponent<PathFollower>().OnReachedDestination += unit => ShopManager.Instance.DecreaseScore(20);
    }

    






}
