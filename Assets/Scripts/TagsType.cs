public class TagsType
{
    private string value;
    private TagsType(string value){
        this.value = value;
    }

    public override string ToString()
    {
        return this.value;
    }
    
    public static TagsType COLLECTABLE {get {return new TagsType("Collectable");}}

    
}
