using System;
using System.Reflection;

namespace ViaEvents
{
    static class Program
    {
        static void Main(string[] args)
        {
            //coздать объект класса Thermostat

            Thermostat thermostat = new Thermostat();
            //coздать объект класса Heater установив начальную температуру равную 30 градусов

            //coздать объект класса Cooler установив начальную температуру равную 40 градусов

            //объект класса Heater - подписаться на событие изменения температуры класса Thermostat

            //объект класса Cooler - подписаться на событие изменения температуры класса Thermostat

            //эмуляция изменения температуры объекта класса Thermostat

            //объект класса Cooler - отписаться от события изменения температуры класса Thermostat

            //эмуляция изменения температуры объекта класса Thermostat на 45 градусов

            Type type = thermostat.GetType();

            foreach (var t in type.GetMethods(BindingFlags.Instance | BindingFlags.Public|BindingFlags.NonPublic ))
            {
                Console.WriteLine(t.Name);
            }
        }
    }

    public class TemperatureArgs : EventArgs
    {
        //TODO
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

    public sealed class Thermostat
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
    }
}
