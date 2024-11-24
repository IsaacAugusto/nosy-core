namespace NosyCore.BrokerChain
{
    public abstract class ChainModifier<T>
    {
        public abstract T Handle(T Data);
    }
    
    public class FloatAddModifier : ChainModifier<float>
    {
        private readonly float _value;
        
        public FloatAddModifier(float value)
        {
            _value = value;
        }

        public override float Handle(float data)
        {
            return data + _value;
        }
    }
    
    public class FloatRatioModifier : ChainModifier<float>
    {
        private readonly float _ratio;
        
        public FloatRatioModifier(float ratio)
        {
            _ratio = ratio;
        }

        public override float Handle(float data)
        {
            return data * _ratio;
        }
    }
}