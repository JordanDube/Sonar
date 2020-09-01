using UnityEngine.UI;
using UnityEngine;
using System.Collections;

public abstract class ChangingObject : MonoBehaviour
{
    [SerializeField] Animation floatAnimation;
    [SerializeField] Animation transformIntoAnimation;
    [SerializeField] Animation transformOutOfAnimation;
    [SerializeField] Animation attackAnimation;
    [SerializeField] Image myImage;
    [SerializeField] bool canHurt;
    [SerializeField] bool canSlow;
    [SerializeField] bool canAttack;
    [SerializeField] Vector3 movePoint1;
    [SerializeField] Vector3 movePoint2;

    public bool isChanged = false;
    public bool canMove
    {
        get => canMove;

        set { gameObject.transform.position = movePoint1;
            movingTo2 = true;
            StartCoroutine(MoveBetweenPoints1To2(movePoint1, movePoint2)); }
    }

    private bool movingTo1 = false;
    private bool movingTo2 = false;

    private IEnumerator MoveBetweenPoints1To2(Vector3 point1, Vector3 point2)
    {
        //Move from point1 to point2
        yield return new WaitForSeconds(0f); //remove later
        //rotate object to 0
        StartCoroutine(MoveBetweenPoints2To1(movePoint1, movePoint2));
    }

    private IEnumerator MoveBetweenPoints2To1(Vector3 point1, Vector3 point2)
    {
        //Move from point1 to point2
        yield return new WaitForSeconds(0f); //remove later
        //Rotate object 180
        StartCoroutine(MoveBetweenPoints1To2(movePoint1, movePoint2));
    }

    public void ResumeNormality()
    {
        //transformOutOfAnimation
        canHurt = false;
        if(canAttack)
        {
            if(movingTo1)
            {
                StartCoroutine(MoveBetweenPoints2To1(gameObject.transform.position, movePoint1));
            }
            else if(movingTo2)
            {
                StartCoroutine(MoveBetweenPoints1To2(gameObject.transform.position, movePoint2));
            }

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag== "Player")
        {
            if(canHurt)
            {
                if(canAttack)
                {
                    StopAllCoroutines();
                    //Play attack coroutine
                }
            }
        }
    }
}
