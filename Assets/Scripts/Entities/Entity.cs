using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BasicObject))]
public class Entity : BasicObject
{

    public enum EntityType
    {
        Neutral,    //0
        Aggresive,  //1
        NeutralAggresive, //2
    }


    //Player interaction
    EntityController ec;
    bool selected;
    bool tookDamage;
    bool largedRadius;

    [SerializeField]
    private float   _entityRadius, 
                    _neutralAgressiveRadius;

    //Entity stats
    [SerializeField]
    private float   _lives,
                    _defense,
                    _strength,
                    _experiences;

    [SerializeField]
    private EntityType _type;

    new protected void Start()
    {
        ec = GetComponent<EntityController>();
        transform.tag = "Entity";

    }

    public void TakeDamage(float damage)
    {
        tookDamage = true;
        Lives -= damage - Defense;
    }

    new protected void Update()
    {
        base.Update();

        if (ec != null)
        {
            switch (Type)
            {
                case EntityType.Neutral:                            //Neutral Behaviour
                    if (selected)
                    {
                        if (tookDamage)
                        {
                            ec.StopPatrol();

                            if (!largedRadius)
                            {
                                EntityRadius *= 2;
                                largedRadius = true;
                            }

                            if (ec.IsPlayerInRange(EntityRadius))
                            {
                                if (ec.IsPlayerReached())
                                {
                                    //Attack
                                }
                                else
                                {
                                    ec.MoveTowardsPlayer();
                                }
                            }
                            else
                            {
                                ec.ResumePatrol();
                            }
                        }
                    }
                    break;
                case EntityType.Aggresive:                          //Agressive Behaviour
                    if (ec.IsPlayerInRange(EntityRadius))
                    {
                        ec.StopPatrol();
                        if (ec.IsPlayerReached())
                        {
                            //Attack
                        }
                        else
                        {
                            ec.MoveTowardsPlayer();
                        }
                    }
                    else if (!ec.IsPatrolPending())
                    {
                        ec.ResumePatrol();
                    }
                    break;
                case EntityType.NeutralAggresive:                   //NeutralAggressive Behaviour
                    if (ec.IsPlayerInRange(NeutralAgressiveRadius))
                    {
                        ec.StopPatrol();
                        if (ec.IsPlayerReached())
                        {
                            //Attack
                        }
                        else if(ec.IsPlayerInRange(EntityRadius))
                        {
                            EntityRadius = NeutralAgressiveRadius;
                            Type = EntityType.Aggresive;
                        }
                    }
                    else if (!ec.IsPatrolPending())
                    {
                        ec.ResumePatrol();
                    }
                    break;
            }
        }



        if (Lives <= 0)
        {
            Destroy(gameObject);
        }
    }

    void OnMouseOver()
    {
        if (Input.GetMouseButton(1))
        {
            selected = true;
        }
    }

    //Get set

    public float Lives
    {
        get
        {
            return _lives;
        }

        set
        {
            _lives = value;
        }
    }

    public float Defense
    {
        get
        {
            return _defense;
        }

        set
        {
            _defense = value;
        }
    }

    public float Strength
    {
        get
        {
            return _strength;
        }

        set
        {
            _strength = value;
        }
    }

    public float Experiences
    {
        get
        {
            return _experiences;
        }

        set
        {
            _experiences = value;
        }
    }

    public EntityType Type
    {
        get
        {
            return _type;
        }

        set
        {
            _type = value;
        }
    }

    public float EntityRadius
    {
        get
        {
            return _entityRadius;
        }

        set
        {
            _entityRadius = value;
        }
    }

    public float NeutralAgressiveRadius
    {
        get
        {
            return _neutralAgressiveRadius;
        }

        set
        {
            _neutralAgressiveRadius = value;
        }
    }
}
