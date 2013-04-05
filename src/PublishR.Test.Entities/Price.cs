using System;
using ProtoBuf;

namespace PublishR.Test.Entities
{
    [ProtoContract, Serializable]
    public class Price
    {
        private string _ticker;
        private double _value;

        public Price()
        {
        }

        public Price(string ticker, double value)
        {
            _ticker = ticker;
            _value = value;
        }


        [ProtoMember(1)]
        public string Ticker
        {
            get { return _ticker; }
            set { _ticker = value; }
        }

        [ProtoMember(2)]
        public double Value
        {
            get { return _value; }
            set { _value = value; }
        }
    }
}