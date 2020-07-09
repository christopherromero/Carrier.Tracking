using System;

namespace Carrier.Tracking
{
    public class FreighterTracking
    {
        private readonly string m_freighterName;
        private double m_weight;
        private double m_capacity;

        public const string OffLoadWeightExceedsCurrentLoadMessage = "Off load weight exceeds current load";
        public const string OffLoadWeightLessThanZeroMessage = "Off load weight is less than zero";

        private FreighterTracking() { }

        public FreighterTracking(string freigherName, double weight)
        {
            m_freighterName = freigherName;
            m_weight = weight;
            m_capacity = 100;
        }

        public string Name
        {
            get { return m_freighterName; }
        }

        public double CurrentLoad
        {
            get { return m_weight; }
        }

        public void OnLoadWeight(double weight)
        {
            if (m_weight + weight > m_capacity)
            {
                throw new ArgumentOutOfRangeException("weight");
            }

            m_weight += weight;
        }

        public void OffLoadWeight(double weight)
        {
            if (weight > m_weight)
            {
                throw new System.ArgumentOutOfRangeException("weight", weight, OffLoadWeightExceedsCurrentLoadMessage);
            }

            if (weight < 0)
            {
                throw new System.ArgumentOutOfRangeException("weight", weight, OffLoadWeightLessThanZeroMessage);
            }

            m_weight -= weight;
        }

        static void Main(string[] args)
        {
            FreighterTracking freighterTracking = new FreighterTracking("Rocinante", 25);

            freighterTracking.OffLoadWeight(5);
            freighterTracking.OnLoadWeight(30);
            Console.WriteLine("The {0}'s current weight is {1}.", freighterTracking.Name, freighterTracking.CurrentLoad);
        }
    }
}
