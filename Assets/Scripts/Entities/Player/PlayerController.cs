using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(NavMeshAgent))]
public class PlayerController : MonoBehaviour
{
    public LayerMask movementMask;      // The ground
    public LayerMask interactionMask;   // Everything we can interact with

    PlayerMotor motor;      // Reference to our motor
    Camera cam;             // Reference to our camera


    // Get references
    void Start()
    {
        motor = GetComponent<PlayerMotor>();
        cam = Camera.main;
    }

    

    // Update is called once per frame
    void Update()
    {

        if (EventSystem.current.IsPointerOverGameObject())
            return;

        // If we press left mouse
        if (Input.GetMouseButtonDown(0))
        {
            // Shoot out a ray
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // If we hit
            if (Physics.Raycast(ray, out hit, movementMask))
            {
                if (hit.transform.GetComponent<BasicObject>().IsInteractable())
                {
                    hit.transform.GetComponent<IInteractable>().Interact(hit.point);
                }
                else
                {
                    motor.MoveToObject(hit.point, false);
                }
                //SetFocus(null);
            }
        }

        

        // If we press right mouse
        if (Input.GetMouseButtonDown(1))
        {
            // Shoot out a ray
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // If we hit
            if (Physics.Raycast(ray, out hit, interactionMask))
            {
                if (hit.transform.GetComponent<BasicObject>().IsInteractable())
                {
                    hit.transform.GetComponent<IInteractable>().ShowGUI(hit.point);
                }
                
            }
        }

        

    }


    /* Focus
// If we press right mouse
if (Input.GetMouseButtonDown(1))
{
   // Shoot out a ray
   Ray ray = cam.ScreenPointToRay(Input.mousePosition);
   RaycastHit hit;

   // If we hit
   if (Physics.Raycast(ray, out hit, 100f, interactionMask))
   {
       SetFocus(hit.collider.GetComponent<Interactable>());
   }
}

}


// Set our focus to a new focus
void SetFocus(Interactable newFocus)
{
if (onFocusChangedCallback != null)
   onFocusChangedCallback.Invoke(newFocus);

// If our focus has changed
if (focus != newFocus && focus != null)
{
   // Let our previous focus know that it's no longer being focused
   focus.OnDefocused();
}

// Set our focus to what we hit
// If it's not an interactable, simply set it to null
focus = newFocus;

if (focus != null)
{
   // Let our focus know that it's being focused
   focus.OnFocused(transform);
}

}
*/
}

/* Old controller
 * {

   public bool rootMotion;

   Ray ray;
   RaycastHit rHit;
   Animator anim;
   UnityEngine.AI.NavMeshAgent nma;
   UnityEngine.AI.NavMeshHit hit;
   RaycastHit rrHit;
   BasicObject obj;
   bool isHidden;

   //Transform enemy;

   public bool running;
   private string state;
   private bool attack;
   private float enemyDistance;

   //private float timer = 0;

   float destinationDistance;
   float stoppingDistance;

   //Temp stats
   public float attackSpeed;
   public float attackStrength;

   void Awake()
   {

       transform.tag = "Player";

       anim = GetComponent<Animator>();
       nma = GetComponent<UnityEngine.AI.NavMeshAgent>();
       running = true;

       isHidden = false;
   }



   void Update()
   {
       bool t = UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject();

       if (Physics.Raycast(transform.position, Vector3.down, out rrHit, 100f))
       {
           if (rrHit.transform.tag == "House")
           {
               obj = (BasicObject)rrHit.transform.GetComponent(typeof(BasicObject));
               obj.HideRoof();
               isHidden = true;
           }
           else if (isHidden && obj != null)
           {
               obj.ShowRoof();
               isHidden = false;
           }
       }

       float distToCamera = Vector3.Distance(Camera.main.transform.position, transform.position);
       Vector3 dirToCamera = Camera.main.transform.position - transform.position;
       //Debug.DrawRay(transform.position, dirToCamera * 1, Color.green);
       RaycastHit[] hits;
       hits = Physics.RaycastAll(transform.position, dirToCamera, distToCamera);

       if (hits.Length > 0)
       {
           foreach (RaycastHit rh in hits)
           {
               rh.transform.GetComponent<BasicObject>().SetTransparent();
           }
       }

       // Movement
       ray = Camera.main.ScreenPointToRay(Input.mousePosition);
       if (Input.GetMouseButton(0) && !t) // Left mouse + no moving over UI
       {
           if (Physics.Raycast(ray, out rHit, 100f, 513))
           {
               MoveToPosition(rHit.point);
           }
       }

       if (Input.GetMouseButtonDown(1)) // Right mouse
       {
           if (Physics.Raycast(ray, out rHit, 100f))
           {
               rHit.transform.GetComponent<IInteractable>().GetDialog();
           }
       }

       destinationDistance = Vector3.Distance(transform.position, rHit.point) - (nma.baseOffset * 1.85f) - nma.stoppingDistance;
       if (rHit.transform != null)
       {
           if (rHit.transform.tag == "Ground")
           {
               nma.stoppingDistance = 0.1f;
               if (destinationDistance <= 0.1f)
               {
                   anim.SetBool("Run", false);
                   nma.isStopped = true;
               }
           }
           else
           {

               stoppingDistance = GetEntityStopRadius(rHit.transform);
               nma.stoppingDistance = stoppingDistance;

               if (destinationDistance <= stoppingDistance)
               {
                   anim.SetBool("Run", false);
                   nma.isStopped = true;
               }
           }
       }
   }

   public void MoveToPosition(Vector3 destination)
   {
       nma.destination = destination;
       anim.SetBool("Run", true);
       nma.isStopped = false;
   }

   float GetEntityStopRadius(Transform entity)
   {
       return entity.GetComponent<BasicObject>().StoppingDistance;
   }
}*/

