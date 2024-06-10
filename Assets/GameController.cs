using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{

    Animator animator; // AnimationType(0 = Idle, 1 = Attacked, 2 = Attacking, 3= charged attack)

    public TextMeshProUGUI DashBoard;
    public TextMeshProUGUI EnemyHP;
    public TextMeshProUGUI PlayerHP;

    private int WasInBossFight = 0;

    private bool Buttons_ON = true;

    // Start is called before the first frame update
    void Start()
    {
        //WasInBossFight = 1; //indicator for loading current position in player scene
       WasInBossFight = 0;
       PlayerPrefs.SetInt("MyIntegerValue", WasInBossFight);
       DashBoard.text = "You start!"; 
       EnemyHP.text = "Enemy HP: 10";
       PlayerHP.text = "Player HP: 10";
       animator = this.GetComponent<Animator>();
    }
    int player_points = 10;
    int ai_points = 15;
    int Wins;
    int Loses;
    int heal = 2;
    bool current_player = true; //true means player and false means AI.
    int charged = 0;
    int defence_activated = 0;
    int m_charged = 0;


    public void setButtons(bool x)
    {
        Buttons_ON = x;
    }

    public bool getButtons()
    {
        return Buttons_ON;
    }


    private IEnumerator BackToIdle()
    {
        yield return new WaitForSeconds(2f);
        animator.SetInteger("AnimationType",0);
    }
 
 
    /*void turn()  // must have some kind of delay
    {
        if (current_player == true)
            {
                current_player = false;
            }
        else
            {
                current_player = true;
            }
    }*/
 
    public void p_attack()
    {
        /*if(m_charged == 1)
        {
            Debug.Log("You are using your special ability that deals a damage of 50% to the enemy!");
            Debug.Log("AI: Ouch! That really hurts!");
            ai_points = (int)Math.Round(0.5 * ai_points);
            m_charged = 0;
        }*/
        int attack = UnityEngine.Random.Range(1,3);
        ai_points -= attack;
        DashBoard.text = "You are attacking the Enemy. You dealed a damage of " + attack + " points.";
        //Delay is needed
        animator.SetInteger("AnimationType",1);                    //Animation= attacked
   
        Debug.Log("The Enemy has " + ai_points + " HP left!");

        if (charged == 1 && ai_points > 0)
        {
            setButtons(false);
            StartCoroutine(BackToIdle());
            StartCoroutine(ai_choice());
        }
        else
        {
            StartCoroutine(BackToIdle());

            result();
            //turn();
            StartCoroutine(ai_choice());
        }
    }
 
    public void p_heal()
    {
        player_points += heal;
        DashBoard.text = "Healing!";
        if (player_points > 10) 
        {
           player_points = 10;
        }
        DashBoard.text = "You just healed you self, and now you have "+ player_points + " HP!";
        StartCoroutine(PlayerHealthUpdater());
        result();
        //turn();
        StartCoroutine(ai_choice()); 
    }
 
     public void p_defend()
    {
        if(charged == 1) // Defending enemy's Charged attack lets you have ult should be used to animate the charging attack but not causing any damage
        {   DashBoard.text = "Enemy is using his charged attack, but you are defending it!";
            animator.SetInteger("AnimationType",3);   
            StartCoroutine(BackToIdle()); 
        //  play Charge attack animation       
            m_charged = 1;
            charged = 0;
            result();
           // turn();
           

        }
        else
        {
            charged = 0;
            defence_activated = 1;
            DashBoard.text = "Defending!";
            result();
            //turn();
            StartCoroutine(ai_choice()); 
        }


    }
 
    private IEnumerator ai_choice()
    {
        //DashBoard.text = "AI: its my turn Hoahoaho(evil laugh)";
        yield return new WaitForSeconds(2f);
        if(charged == 1)
        {
            // delay is needed here
            setButtons(false);
            DashBoard.text ="AI is using his charging power to eliminate you!";
            player_points = 0;
            animator.SetInteger("AnimationType",3);   
            StartCoroutine(BackToIdle());
            StartCoroutine(PlayerHealthUpdater());
            StartCoroutine(SceneExitDelayed());
        }
        int a = UnityEngine.Random.Range(1,101);
        //defence_activated = 0;
        if (a > 35 && player_points > 0) // attack
        {
            if (defence_activated == 0)
            {
                int attack = UnityEngine.Random.Range(1,3);
                player_points -= attack;
                DashBoard.text = "AI is attacking you!";
                //DashBoard.text = "AI is attacking you!. You have " + player_points + " HP left.";
                animator.SetInteger("AnimationType",2);  
                defence_activated = 0;
                if (player_points <= 0)
                {
                    StartCoroutine(BackToIdle());
                    StartCoroutine(PlayerHealthUpdater());
                    StartCoroutine(Losing());
                    StartCoroutine(SceneExit());
                }
                else
                {
                    StartCoroutine(BackToIdle());
                    StartCoroutine(PlayerHealthUpdater());
                    result();
                }
            }
            else
            {
                DashBoard.text = "AI is attacking you!. But you defended";
                defence_activated = 0;
                animator.SetInteger("AnimationType",2);
                StartCoroutine(BackToIdle());
                StartCoroutine(PlayerHealthUpdater());
                result();
            }

        }
        else if(a <= 35 && player_points > 0)//charge 
        {
            charged = 1;
            DashBoard.text = ("WARNING ||YOUR ENEMY IS CHARGING|| WARNING");
            defence_activated = 0;
            result();
        }

        //turn();
    }
    
    void result()
    {
        if (player_points <= 0)
        {
            Debug.Log("BOOM!");
            DashBoard.text = "You losed!";
            //Loses+=1;
            //reset();
            PlayerHP.text = "Player HP: " + player_points;
            WasInBossFight = 1;
            PlayerPrefs.SetInt("MyIntegerValue", WasInBossFight);
            StartCoroutine(SceneExit());
        }
        else if(ai_points <= 0)
        {
            DashBoard.text = "You won";
            //Wins+=1;
            //reset();
            Debug.Log("Its your Turn, ladies first.");
            Debug.Log("Im joking xD");
            WasInBossFight = 1;
            PlayerPrefs.SetInt("MyIntegerValue", WasInBossFight);
            StartCoroutine(SceneExit());
        }
    }

    public void p_Run()
    {
        WasInBossFight = 1;
        PlayerPrefs.SetInt("MyIntegerValue", WasInBossFight);
        SceneManager.LoadScene(0);
    }


    private IEnumerator SceneExit()
    {
        yield return new WaitForSeconds(3f);
        WasInBossFight = 1;
        PlayerPrefs.SetInt("MyIntegerValue", WasInBossFight);
        SceneManager.LoadScene(0);
    }

    private IEnumerator SceneExitDelayed()
    {
        yield return new WaitForSeconds(7f);
        WasInBossFight = 1;
        PlayerPrefs.SetInt("MyIntegerValue", WasInBossFight);
        SceneManager.LoadScene(0);
    }

    /*    void reset()
        {
            Debug.Log("The game has finished. The main results are "+ Wins + " wins for you and " + Loses + " for the AI");
            Debug.Log("The game has been restarted");
            player_points = 10;
            ai_points = 10;
            heal = 2;
            current_player = true; //true means player and false means AI.
            charged = 0;
            defence_activated = 0;
            //turn();
        }*/

    

    // Update is called once per frame
    void Update()
    {
        EnemyHP.text = "Enemy HP: " + ai_points;
       // PlayerHP.text = "Player HP: " + player_points;
 
        //player
        /*
        if(Input.GetKeyDown(KeyCode.A) && current_player == true)
        {
            p_attack();
        }
 
 
        if(Input.GetKeyDown(KeyCode.S) && current_player == true)
        {
            p_heal();
        }
 
        if(Input.GetKeyDown(KeyCode.D) && current_player == true)
        {
            p_defend();
        }
        
 
        //AI
        if(current_player == false) 
        {
            StartCoroutine(ai_choice()); 
        }
        */
    }

    private IEnumerator Losing()
    {
        yield return new WaitForSeconds(3f);
        DashBoard.text = "You losed!";
    }


    private IEnumerator PlayerHealthUpdater()
    {
        yield return new WaitForSeconds(2f);
        PlayerHP.text = "Player HP: " + player_points;
    }
 
}