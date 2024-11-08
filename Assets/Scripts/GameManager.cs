using System.Collections;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private Battler battler1;
    private Battler battler2;

    private int RoundNumber;

    #region Player1
    [SerializeField] private TextMeshProUGUI player1Name;
    [SerializeField] private TextMeshProUGUI player1Lifes;
    [SerializeField] private TextMeshProUGUI player1Attack;
    [SerializeField] private TextMeshProUGUI player1Deffense;

    #endregion

    #region Player2
    [SerializeField] private TextMeshProUGUI player2Name;
    [SerializeField] private TextMeshProUGUI player2Lifes;
    [SerializeField] private TextMeshProUGUI player2Attack;
    [SerializeField] private TextMeshProUGUI player2Deffense;
    #endregion

    [SerializeField] private TextMeshProUGUI RounCounterText;

    private void Awake()
    {
        Instance = this;
    }

    public void SetBattlers(Battler battler)
    {
        if (battler1 == null)
        {
            battler1 = battler;
            player1Attack.name = $"Name: {battler.gameObject.name}";
            player1Lifes.text = $"Lifes: {battler.GetLives()}";
            player1Attack.text = $"Attack: {battler.GetAttack()}";
            player1Deffense.text = $"Deffense: {battler.GetDeffense()}";
        }
        else
        {
            battler2 = battler;
            player2Attack.name = $"Name: {battler.gameObject.name}";
            player2Lifes.text = $"Lifes: {battler.GetLives()}";
            player2Attack.text = $"Attack: {battler.GetAttack()}";
            player2Deffense.text = $"Deffense: {battler.GetDeffense()}";
        }
    }

    public void StartBattle()
    {
        battler1.LookAtOpponent(battler2.transform);
        battler2.LookAtOpponent(battler1.transform);
        StartCoroutine(HandleBattle());
    }

    private IEnumerator HandleBattle()
    {
        while (battler1 != null && battler2 != null)
        {
            RounCounterText.text = $"ROUND: {RoundNumber}";
            yield return new WaitForSeconds(2f);

            battler1.ChooseAttack();
            player1Attack.text = $"Attack: {battler1.GetAttack()}";
            player1Deffense.text = $"Deffense: {battler1.GetDeffense()}";

            battler2.ChooseAttack();
            player2Attack.text = $"Attack: {battler2.GetAttack()}";
            player2Deffense.text = $"Deffense: {battler2.GetDeffense()}";


            if (battler1.GetIsAttacking() == false && battler2.GetIsAttacking() == false)
            {
                RoundNumber++;
                continue;
            }

            if(battler1.GetAttack() > battler2.GetDeffense())
            {
                battler2.TakeDamage();
                player2Lifes.text = $"Lifes: {battler2.GetLives()}";
            }

            if(battler2.GetAttack() > battler1.GetDeffense())
            {
                battler1.TakeDamage();
                player1Lifes.text = $"Lifes: {battler1.GetLives()}";
            }
            RoundNumber++;
        }
    }
}
