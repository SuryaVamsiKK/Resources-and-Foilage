namespace UnityEngine.PostProcessing
{
    public sealed class MinsAttributeAttribute : PropertyAttribute
    {
        public readonly float min;

        public MinsAttributeAttribute(float min)
        {
            this.min = min;
        }
    }
}
