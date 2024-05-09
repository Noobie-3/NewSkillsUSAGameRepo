using UnityEngine;

public class KnockBackForceField : MonoBehaviour
{
    public float knockBackForce;
    private bool isBeingKnockedBack = false;
    private float distance;
    public Boss_StateMachine boss;
    public Vector3 knockbackDirection;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == GameController.instance.Player)
        {
            boss.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            isBeingKnockedBack = true;
            GameController.instance.Player.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        }
    }

    private void Update()
    {
        distance = Vector3.Distance(GameController.instance.Player.transform.position, transform.position);
        

        if (isBeingKnockedBack)
        {
             knockbackDirection = (GameController.instance.Player.transform.position - transform.position);
            knockbackDirection.y = 0;
            GameController.instance.Player.GetComponent<REVAMPEDPLAYERCONTROLLER>().ApplyKnockback(knockbackDirection * knockBackForce);

            if (distance > 15)
            {
                isBeingKnockedBack = false;
                GameController.instance.Player.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
                if (boss != null)
                {
                    if (boss.gameObject.GetComponent<Rigidbody>().isKinematic == true)
                    {
                        boss.gameObject.GetComponent<Rigidbody>().isKinematic = false;
                    }
                }
            }
        }
    }
}
