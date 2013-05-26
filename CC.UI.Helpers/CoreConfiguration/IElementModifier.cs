namespace CC.UI.Helpers.CoreConfiguration
{
    public interface IElementModifier
    {
        TagModifier CreateModifier(AccessorDef accessorDef);
    }
}
