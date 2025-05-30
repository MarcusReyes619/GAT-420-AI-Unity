using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Mediator
{
   public void Request(Concrete concrete);
    public void Dead(Concrete concrete);

}

public interface Concrete
{
    public void Request();
    public void Died();
    public void Notify(string message);
}

public class AiConcrete : Concrete
{
    AiMediator aiMediator;

    public AiConcrete(AiMediator aiMediator)
    {
        this.aiMediator = aiMediator;
    }

    public void Died()
    {
        aiMediator.Dead(this);
    }

    public void Notify(string message)
    {
        Debug.Log(message);
    }

    
    public void Request()
    {
        aiMediator.Request(this);
        
    }
}

public class AiMediator : Mediator
{
    public void Dead(Concrete concrete)
    {
        concrete.Notify(concrete + ":Died");
    }

    public void Request(Concrete concrete)
    {
        concrete.Notify(concrete + ": Spawned");
    }
}