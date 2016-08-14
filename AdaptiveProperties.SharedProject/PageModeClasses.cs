namespace AdaptiveProperties
{
    public enum PageMode { XS, XL }
    public abstract class PageModeClass
    {
        public abstract PageMode PageMode { get; }
    }

    public class PageModeXSClass : PageModeClass
    {
        public override PageMode PageMode => PageMode.XS;
    }

    public class PageModeXLClass : PageModeClass
    {
        public override PageMode PageMode => PageMode.XL;
    }
}
