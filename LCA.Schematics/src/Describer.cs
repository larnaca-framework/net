namespace LCA.Schematics
{
    /// <summary>
    /// Class responsible 
    /// </summary>
    public class Describer
    {
        public static Describer Default { get; } = new Describer();

        private Model _model = new Model();
    }
}