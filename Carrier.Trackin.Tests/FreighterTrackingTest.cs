using Microsoft.VisualStudio.TestTools.UnitTesting;
using Carrier.Tracking;
namespace Carrier.Trackin.Tests
{
    [TestClass]
    public class FreighterTrackingTest
    {
        [TestMethod]
        public void OffLoadWeight_WithValidAmount_UpdatesWeight()
        {
            // Arrange 
            double beginningWeight = 70;
            double offloadedWeight = 12;
            double expectedWeight = 58;

            FreighterTracking freighterTracking = new FreighterTracking("Rocinante", beginningWeight, 20);

            // Act
            freighterTracking.OffLoadWeight(offloadedWeight);

            // Assert
            double actual = freighterTracking.CurrentLoad;
            Assert.AreEqual(expectedWeight, actual, "Weights not adjusted correctly");
        }

        [TestMethod]
        public void OffLoadWeight_WhenWeightIsMoreThanCurrentLoad_ShouldThrowArgumentOutOfRange()
        {
            // Arrange
            double beginningWeight = 92;
            double offloadedWeight = 93;
            FreighterTracking account = new FreighterTracking("Millennium  Falcon", beginningWeight, 66);

            // Act
            try
            {
                account.OffLoadWeight(offloadedWeight);
            }
            catch (System.ArgumentOutOfRangeException e)
            {
                // Assert
                StringAssert.Contains(e.Message, FreighterTracking.OffLoadWeightExceedsCurrentLoadMessage);
                return;
            }

            Assert.Fail("The expected exception was not thrown.");
        }

        [TestMethod]
        public void CalculateFreightDistance_WithValidDistance()
        {
            // Arrange 
            double beginningWeight = 70.65;
            float originLocation = 12;
            float proposedDestination = 58;
            float expectedDestination = 46;

            FreighterTracking freighterTracking = new FreighterTracking("Rocinante", beginningWeight, originLocation);

            // Act
            double actual = freighterTracking.CalculateFreightDistance(proposedDestination);

            // Assert
            Assert.AreEqual(expectedDestination, actual, "Destination not adjusted correctly");
        }

        [TestMethod]
        public void CalculateFreightDistance_WithInvalidDistance_ShouldThrowArgumentOutOfRange()
        {
            // Arrange
            double beginningWeight = 92;
            float originLocation = 66;
            float proposedDestination = -23;

            FreighterTracking freighterTracking = new FreighterTracking("Millennium  Falcon", beginningWeight, originLocation);

            double actual;

            // Act
            try
            {
                actual = freighterTracking.CalculateFreightDistance(proposedDestination);
            }
            catch (System.ArgumentOutOfRangeException e)
            {
                // Assert
                StringAssert.Contains(e.Message, FreighterTracking.DestinationLessThanZeroMessage);
                return;
            }

            Assert.Fail("The expected exception was not thrown.");
        }
    }
}
