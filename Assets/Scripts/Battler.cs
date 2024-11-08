using UnityEngine;

public class Battler : MonoBehaviour
{
    private int lives = 5;
    private int attack = 0;
    private int deffense = 0;
    private bool isAttacking = false;

    private void Start()
    {
        GameManager.Instance.SetBattlers(this);
    }

    public void ChooseAttack()
    {
        int randomNum = Random.Range(0, 1);

        if(randomNum == 0)
        {
            isAttacking = true;
            SetAttackPower();
        }
        else
        {
            isAttacking = false;
            SetDeffensePower();
        }
    }

    private void SetAttackPower()
    {
        attack = Random.Range(0, 10);
    }

    private void SetDeffensePower()
    {
        deffense = Random.Range(0, 10);
    }

    public void TakeDamage()
    {
        lives--;
        if( lives <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    public void LookAtOpponent(Transform opponentPosition)
    {
        transform.LookAt(opponentPosition.position);
    }

    public int GetLives() => lives;
    public bool GetIsAttacking() => isAttacking;
    public int GetAttack() => attack;
    public int GetDeffense() => deffense;
}
