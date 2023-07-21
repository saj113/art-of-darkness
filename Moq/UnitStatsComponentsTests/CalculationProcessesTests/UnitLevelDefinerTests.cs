using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.UnitStatsComponents.CalculationProcesses;
using Assets.Scripts.Utilities;
using NUnit.Framework;

namespace Moq.UnitStatsComponentsTests.CalculationProcessesTests
{
    [TestFixture]
    internal class UnitLevelDefinerTests
    {
        private MockRepository _mockRepository;

        [TestFixtureSetUp]
        public void Init()
        {
            _mockRepository = new MockRepository(MockBehavior.Loose);
        }

        [SetUp]
        public void Setup()
        {

        }

        [Test]
        [TestCase(UnitLevelDefinerAlgorithm.OnCharacterLevel)]
        [TestCase(UnitLevelDefinerAlgorithm.OnLayerLevel)]
        public void CorrectAlgorithmSelectedTest(UnitLevelDefinerAlgorithm algorithm)
        {
            var onCharacterLevelDefinerMock = GetOnCharacterLevelDefinerMock();
            var onLayerLevelDefinerMock = GetOnLayerLevelDefinerMock();
            onLayerLevelDefinerMock
                .SetupGet(p => p.LayersInfo)
                .Returns(Enumerable.Empty<LayerInfo>());

            var unitLevelDefiner = new UnitLevelDefiner(
                onCharacterLevelDefinerMock.Object, onLayerLevelDefinerMock.Object);

            unitLevelDefiner.DetectLevel(algorithm);

            if (algorithm == UnitLevelDefinerAlgorithm.OnCharacterLevel)
            {
                onCharacterLevelDefinerMock.Verify(p => p.CharacterLevel, Times.Once());
                onLayerLevelDefinerMock.Verify(p => p.LayersInfo, Times.Never());
                onLayerLevelDefinerMock.Verify(p => p.LayerIndex, Times.Never());
                onLayerLevelDefinerMock.Verify(p => p.LayerLevel, Times.Never());
            }
            else
            {
                onCharacterLevelDefinerMock.Verify(p => p.CharacterLevel, Times.Never());
                onLayerLevelDefinerMock.Verify(p => p.LayersInfo, Times.AtLeastOnce());
                onLayerLevelDefinerMock.Verify(p => p.LayerIndex, Times.AtLeastOnce());
                onLayerLevelDefinerMock.Verify(p => p.LayerLevel, Times.AtLeastOnce());
            }
        }

        [Test]
        public void OnCharacterLevelAlgorithm_ValidResultTest()
        {
            var onCharacterLevelDefinerMock = GetOnCharacterLevelDefinerMock();
            var onLayerLevelDefinerMock = GetOnLayerLevelDefinerMock();

            var expectedLevel = 10;
            onCharacterLevelDefinerMock
                .SetupGet(p => p.CharacterLevel).Returns(expectedLevel);
            var unitLevelDefiner = new UnitLevelDefiner(
                onCharacterLevelDefinerMock.Object, onLayerLevelDefinerMock.Object);

            var actualLevel = unitLevelDefiner.DetectLevel(
                UnitLevelDefinerAlgorithm.OnCharacterLevel);

            Assert.AreEqual(expectedLevel, actualLevel);
        }

        [Test]
        public void OnOnLayerLevelAlgorithm_LayersInfoIsEmptyTest()
        {
            var onCharacterLevelDefinerMock = GetOnCharacterLevelDefinerMock();
            var onLayerLevelDefinerMock = GetOnLayerLevelDefinerMock();

            var expectedLevel = 0;
            onLayerLevelDefinerMock
                .SetupGet(p => p.LayersInfo)
                .Returns(Enumerable.Empty<LayerInfo>());
            var unitLevelDefiner = new UnitLevelDefiner(
                onCharacterLevelDefinerMock.Object, onLayerLevelDefinerMock.Object);

            var actualLevel = unitLevelDefiner.DetectLevel(
                UnitLevelDefinerAlgorithm.OnLayerLevel);

            Assert.AreEqual(expectedLevel, actualLevel);
        }

        [Test]
        public void OnOnLayerLevelAlgorithm_ValidLayerInfoTest()
        {
            var onCharacterLevelDefinerMock = GetOnCharacterLevelDefinerMock();
            var onLayerLevelDefinerMock = GetOnLayerLevelDefinerMock();

            var expectedLevel = 17;
            onLayerLevelDefinerMock
                .SetupGet(p => p.LayersInfo)
                .Returns(GetValidLayerInfos());
            var unitLevelDefiner = new UnitLevelDefiner(
                onCharacterLevelDefinerMock.Object, onLayerLevelDefinerMock.Object);

            var actualLevel = unitLevelDefiner.DetectLevel(
                UnitLevelDefinerAlgorithm.OnLayerLevel);

            Assert.AreEqual(expectedLevel, actualLevel);
        }

        [Test]
        [TestCase(1, 0)]
        [TestCase(2, 10)]
        [TestCase(3, 18)]
        public void OnOnLayerLevelAlgorithm_ValidLayerIndexPropertyTest(
            int layerIndex, int expectedLevel)
        {
            var onCharacterLevelDefinerMock = GetOnCharacterLevelDefinerMock();
            var onLayerLevelDefinerMock = GetOnLayerLevelDefinerMock();

            onLayerLevelDefinerMock
                .SetupGet(p => p.LayersInfo)
                .Returns(GetValidLayerInfos());
            onLayerLevelDefinerMock
                .SetupGet(p => p.LayerIndex)
                .Returns(layerIndex);
            var unitLevelDefiner = new UnitLevelDefiner(
                onCharacterLevelDefinerMock.Object, onLayerLevelDefinerMock.Object);

            var actualLevel = unitLevelDefiner.DetectLevel(
                UnitLevelDefinerAlgorithm.OnLayerLevel);

            Assert.AreEqual(expectedLevel, actualLevel);
        }

        [Test]
        [TestCase(1, 3, 3)]
        [TestCase(2, 4, 14)]
        [TestCase(3, 5, 20)]
        public void OnOnLayerLevelAlgorithm_ValidLayerLevelPropertyTest(
            int layerIndex, int layerLevel, int expectedLevel)
        {
            var onCharacterLevelDefinerMock = GetOnCharacterLevelDefinerMock();
            var onLayerLevelDefinerMock = GetOnLayerLevelDefinerMock();

            onLayerLevelDefinerMock
                .SetupGet(p => p.LayersInfo)
                .Returns(GetValidLayerInfos());
            onLayerLevelDefinerMock
                .SetupGet(p => p.LayerIndex)
                .Returns(layerIndex);
            onLayerLevelDefinerMock
                .SetupGet(p => p.LayerLevel)
                .Returns(layerLevel);
            var unitLevelDefiner = new UnitLevelDefiner(
                onCharacterLevelDefinerMock.Object, onLayerLevelDefinerMock.Object);

            var actualLevel = unitLevelDefiner.DetectLevel(
                UnitLevelDefinerAlgorithm.OnLayerLevel);

            Assert.AreEqual(expectedLevel, actualLevel);
        }

        private IEnumerable<LayerInfo> GetValidLayerInfos()
        {
            return new List<LayerInfo>
            {
                new LayerInfo(1, 10, 10, 10),
                new LayerInfo(2, 5, 5, 5),
                new LayerInfo(3, 2, 2, 2)
            };
        }

        private Mock<IOnCharacterLevelDefiner> GetOnCharacterLevelDefinerMock()
        {
            return _mockRepository.Create<IOnCharacterLevelDefiner>();
        }

        private Mock<IOnLayerLevelDefiner> GetOnLayerLevelDefinerMock()
        {
            return _mockRepository.Create<IOnLayerLevelDefiner>();
        }
    }
}
