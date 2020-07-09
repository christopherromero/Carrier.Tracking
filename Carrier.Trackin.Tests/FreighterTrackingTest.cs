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

            FreighterTracking freighterTracking = new FreighterTracking("Rocinante", beginningWeight);

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
            FreighterTracking account = new FreighterTracking("Millennium  Falcon", beginningWeight);

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
    }
}
