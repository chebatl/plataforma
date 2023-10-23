public class CollectableType
{   

    private string value;
    private CollectableType(string value){
        this.value = value;
    }

    public override string ToString()
    {
        return this.value;
    }
    
    public static CollectableType AMMO {get {return new CollectableType("Ammo");}}
    public static CollectableType EGG {get {return new CollectableType("Egg");}}

}
