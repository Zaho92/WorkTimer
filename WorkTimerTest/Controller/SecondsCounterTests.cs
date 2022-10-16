using WorkTimer.Controller;

namespace WorkTimerTest.Controller
{
    public class SecondsCounterTests
    {
        [Fact]
        public void Run_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var secondsCounter = new SecondsCounter();

            // Act
            secondsCounter.Run();

            // Assert
            Assert.True(secondsCounter.IsRunning);
        }

        [Fact]
        public void Pause_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var secondsCounter = new SecondsCounter();

            // Act
            secondsCounter.Pause();

            // Assert
            Assert.False(secondsCounter.IsRunning);
        }

        [Fact]
        public void Reset_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var secondsCounter = new SecondsCounter();

            // Act
            secondsCounter.Reset();

            // Assert
            Assert.True(!secondsCounter.IsRunning && secondsCounter.Seconds == 0);
        }

        [Theory]
        [InlineData(11)]
        [InlineData(104)]
        [InlineData(750)]
        [InlineData(1000)]
        [InlineData(2000)]
        [InlineData(2500)]
        [InlineData(3200)]
        [InlineData(4000)]
        [InlineData(6781)]
        public void RunningPrecision_StateUnderTest_ExpectedBehavior(int ms)
        {
            // Arrange
            var secondsCounter = new SecondsCounter();

            // Act
            secondsCounter.Run();
            // Etwas Puffer: Bei exaktem stoppen nach 2000ms kann in diesem Timer-Ansatz noch 1 Sekunde eingetragen sein, da das Enabled=false dann kurz vor dem Invoken des Events passiert.
            // Da das kein realistischer Fall ist, geben wir einen kleinen Puffer. Nach den 20ms sollte die korrekte Sekundenzahl eingetragen sein.
            Thread.Sleep(ms + 20);
            secondsCounter.Pause();

            // Assert
            int expectedSeconds = ms / 1000;

            Assert.True(secondsCounter.Seconds == expectedSeconds);
        }
    }
}