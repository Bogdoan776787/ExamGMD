using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTwo : Enemy
{
 
    public float minX;
    public float maxX;
    public float minY;
    public float maxY;

    private Vector2 targetPosition;
    private Animator anim;

    public float timeBetweenEnemyTwo;

    private float enemyTwoTime;

    public Enemy enemyToEnemyTwo;

    public float attackSpeed;
    public float stopDistance;
    private float attackTime;
    public override void Start()
    {
        base.Start();
        float randomX = Random.Range(minX, maxX);
        float randomY = Random.Range(minY, maxY);
        targetPosition = new Vector2(randomX, randomY);
        anim = GetComponent<Animator>();
        
    }

    private void Update()
    {
        if (player != null)
        {
            if (Vector2.Distance(transform.position, targetPosition)>.5f)
            {
                transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
                anim.SetBool("isRunning", true);
            }
            else
            {
                anim.SetBool("isRunning", false);
                if (Time.time >= enemyTwoTime)
                {
                    enemyTwoTime = Time.time + timeBetweenEnemyTwo;
                    anim.SetTrigger("jumpSummoner");
                }
            }
            if (Vector2.Distance(transform.position, player.position)< stopDistance)
            {
                if (Time.time >= attackTime)
                {
                    StartCoroutine(Attack());
                    //attack
                    attackTime = Time.time + timeBetweenAttacks;
                }
            }
        }
    }

    public void Berserc()
    {
        if (player != null)
        {
            Instantiate(enemyToEnemyTwo, transform.position, transform.rotation);
        }
    }
    IEnumerator Attack() {
        player.GetComponent<Player>().TakeDamage(damage);

        Vector2 originalPosition = transform.position;
        Vector2 targetPosition = player.position;
        float percent = 0;
        while (percent <= 1)
        {
            percent += Time.deltaTime * attackSpeed;
            float formula = (-Mathf.Pow(percent, 2) + percent) * 4;
            transform.position = Vector2.Lerp(originalPosition, targetPosition, formula);
            yield return null;
        }
    }
}
