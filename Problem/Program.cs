using System;

namespace Problem
{
    static class Program
    {
        static void Main(string[] args)
        {
            Thermostat thermostat = new Thermostat();

            Heater heater = new Heater(30);

            Cooler cooler = new Cooler(40);

            thermostat.EmulateTemperatureChange();

            heater.OnTemperatureChanged(thermostat.CurrentTemperature);

            cooler.Update(thermostat.CurrentTemperature);

            thermostat.EmulateTemperatureChange();

            heater.OnTemperatureChanged(thermostat.CurrentTemperature);

            cooler.Update(thermostat.CurrentTemperature);
        }
    }

    public class Cooler
    {
        public Cooler(int temperature) => Temperature = temperature;

        private int Temperature { get; set; }

        public void Update(int newTemperature)
            =>
                Console.WriteLine(newTemperature > Temperature
                    ? $"Cooler: On. Changed:{Math.Abs(newTemperature - Temperature)}"
                    : $"Cooler: Off. Changed:{Math.Abs(newTemperature - Temperature)}");
    }

    public class Heater
    {
        public Heater(int temperature) => Temperature = temperature;

        private int Temperature { get; set; }

        public void OnTemperatureChanged(int newTemperature)
        {
            Console.WriteLine(newTemperature < Temperature
                ? $"Heater: On. Changed:{Math.Abs(newTemperature - Temperature)}"
                : $"Heater: Off. Changed:{Math.Abs(newTemperature - Temperature)}");
        }
    }

    public class Thermostat
    {
        private int currentTemperature;

        private readonly Random random = new Random(Environment.TickCount);

        public Thermostat() => currentTemperature = random.Next(0, 100);

        public int CurrentTemperature
        {
            get => currentTemperature;
            private set
            {
                if (value > currentTemperature)
                {
                    currentTemperature = value;
                }
            }
        }

        public void EmulateTemperatureChange() => this.CurrentTemperature = random.Next(0, 100);
    }
}