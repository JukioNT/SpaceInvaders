using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InvadersSpawner : MonoBehaviour
{
    public float spaceBetweenCol = 2.0f;
    public float spaceBetweenRow = 2.0f;
    public Invader[] prefabs;
    public int rows = 5;
    public int columns = 11;
    public int invadersKilled { get; private set; }
    public int invadersAlive => this.totalInvaders - this.invadersKilled;
    public int totalInvaders => this.rows * this.columns;
    public float killsPercentage => (float)this.invadersKilled / (float)this.totalInvaders;

    public Laser missile;
    private Vector3 direction = Vector2.right;
    public AnimationCurve speed;
    public float descendingValue = 1.0f;
    public float missileAttackRate = 1.0f;

    private void Awake()
    {
        for(int  row = 0; row < rows; row++)
        {
            float width = spaceBetweenCol * (this.columns - 1);
            float height = spaceBetweenRow * (this.rows - 1);
            Vector2 centering = new Vector2(-width /2, -height/2);
            Vector3 rowPosition = new Vector3(centering.x, centering.y + (row * spaceBetweenRow), 0.0f);

            for(int col = 0; col < columns; col++)
            {
                Invader invader = Instantiate(this.prefabs[row], this.transform);
                invader.killed += InvaderKiller;
                Vector3 position = rowPosition;
                position.x += col * spaceBetweenCol;
                invader.transform.localPosition = position;
            }
        }
    }

    private void Start()
    {
        InvokeRepeating(nameof(MissileAttack), this.missileAttackRate, this.missileAttackRate);
    }

    private void InvaderKiller()
    {
        this.invadersKilled++;

        if(this.invadersAlive <= 0)
        {
            SceneManager.LoadScene("Start");
        }
    }

    //Movimentation

    private void FixedUpdate()
    {
        this.transform.position += direction * this.speed.Evaluate(this.killsPercentage) * Time.deltaTime;
    }

    private void Update()
    {

        Vector3 leftEdge = Camera.main.ViewportToWorldPoint(Vector3.zero);
        Vector3 rightEdge = Camera.main.ViewportToWorldPoint(Vector3.right);

        foreach (Transform invader in this.transform)
        {
            if (!gameObject.activeInHierarchy)
            {
                continue;
            }

            if (direction == Vector3.right && invader.position.x >= (rightEdge.x - 2.0f))
            {
                AdvanceDirection();
            }
            else if (direction == Vector3.left && invader.position.x <= (leftEdge.x + 2.0f))
            {
                AdvanceDirection();
            }
        }
    }

    private void MissileAttack()
    {
        foreach (Transform invader in this.transform)
        {
            if (!gameObject.activeInHierarchy)
            {
                continue;
            }

            if(Random.value < (1.0f / (float)this.invadersAlive))
            {
                Instantiate(this.missile, invader.position, Quaternion.Euler(0, 180, 0));
                break;
            }
        }
    }

    private void AdvanceDirection()
    {
        if(gameObject.transform.position.y <= -3)
        {
            SceneManager.LoadScene("Start");
        }
        direction.x *= -1.0f;
        Vector3 position = this.transform.position;
        position.y -= descendingValue;
        this.transform.position = position;
    }
}


