using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float health = 100;
    public FiniteStateMachine fsm;

    void Start()
    {
        fsm = new FiniteStateMachine();

        fsm.AddState("Hurt", new State( onEnter: state => { health -= 1; }, instantTransition: true));
        fsm.AddState("Walking", new State());
        fsm.AddState("Rest", new State());
        fsm.AddState("InAction", new State());

        fsm.AddTwoWayTransition(new Transition(null, "Hurt", condition: state => { return Input.GetKeyDown(KeyCode.A); }));

        fsm.AddTransition(new Transition(null, "InAction", condition: state => { return Input.GetKeyDown(KeyCode.E); }));
        fsm.AddTransition(new Transition("InAction", null, condition: state => { return Input.GetKeyDown(KeyCode.X); }));

        fsm.AddTransition(new Transition("Rest", "Walking", condition: state => { return Input.GetKeyDown(KeyCode.W); }));
        fsm.AddTransition(new Transition("Walking", "Rest", condition: state => { return Input.GetKeyDown(KeyCode.W); }));

        fsm.SetStartState("Rest");

        fsm.Init();
    }

    void Update()
    {
        fsm.Update();
        Debug.Log("Player's Health is: " + health + " and is in State " + fsm.activeState.GetName() + "!");
        //Debug.Log("Player's Health is: " + health + " and is in State " + fsm.activeState.GetName() + " And was in this state for " + fsm.activeState.GetElapsedTime() + "ms!");
    }
}
