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
    public static TagsType THROWABLE {get {return new TagsType("Throwable");}}
    public static TagsType GROUND {get {return new TagsType("Ground");}}

    
}
