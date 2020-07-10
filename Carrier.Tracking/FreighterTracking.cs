using System;

namespace Carrier.Tracking
{
    public class FreighterTracking
    {
        private readonly string m_freighterName;
        private double m_weight;
        private double m_capacity;
        private float m_origin;

        public const string OffLoadWeightExceedsCurrentLoadMessage = "Off load weight exceeds current load";
        public const string OffLoadWeightLessThanZeroMessage = "Off load weight is less than zero";
        public const  string DestinationLessThanZeroMessage = "Destination is less than zero";


        private FreighterTracking() { }

        public FreighterTracking(string freigherName, double weight, float origin)
        {
            m_freighterName = freigherName;
            m_weight = weight;
            m_origin = origin;

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

        public float CalculateFreightDistance(float destination)
        {
            if(destination < m_origin)
            {
                throw new System.ArgumentOutOfRangeException("destination", destination, DestinationLessThanZeroMessage);
            }

            return destination - m_origin;
        }

        static void Main(string[] args)
        {
            FreighterTracking freighterTracking = new FreighterTracking("Rocinante", 25, 1234);

            freighterTracking.OffLoadWeight(5);
            freighterTracking.OnLoadWeight(30);
            var proposedDistance = freighterTracking.CalculateFreightDistance(2000);

            Console.WriteLine("The {0}'s current weight is {1}.", freighterTracking.Name, freighterTracking.CurrentLoad);
            Console.WriteLine("The {0}'s current location is {1} and will travel {2} miles.", freighterTracking.Name, freighterTracking.CurrentLoad, proposedDistance);
        }
    }
}
