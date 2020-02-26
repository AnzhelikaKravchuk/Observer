using System;

namespace ViaInterfaces.Solution._2
{
    static class Program
    {
        static void Main(string[] args)
        {
            //coздать объект класса Thermostat

            //coздать объект класса Heater установив начальную температуру равную 30 градусов

            //coздать объект класса Cooler установив начальную температуру равную 40 градусов

            //объект класса Heater - подписаться на событие изменения температуры класса Thermostat

            //объект класса Cooler - подписаться на событие изменения температуры класса Thermostat

            //эмуляция изменения температуры объекта класса Thermostat

            //объект класса Cooler - отписаться от события изменения температуры класса Thermostat

            //эмуляция изменения температуры объекта класса Thermostat на 45 градусов
        }
    }

    //интерфейс класса наблюдателя
    public interface IObserver
    {
        void Update(IObservable sender, TemperatureEventArgs eventArgs);
    }

    //интерфейс наблюдаемого класса 
    public interface IObservable
    {
        void Register(IObserver observer);
        void Unregister(IObserver observer);
        void Notify();
    }

    //базовый класс, предоставляющий дополнительную информацию о событии
    // для событий не предоставляющих дополнительную информацию свойство Empty
    public class EventArgs
    {
        public static readonly EventArgs Empty;
    }

    // класс, предоставляющий дополнительную информацию о событии изменения температуры
    public class TemperatureEventArgs : EventArgs
    {
        public TemperatureEventArgs(int newTemperature, int oldTemperature)
        {
            NewTemperature = newTemperature;
            OldTemperature = oldTemperature;
        }

        public int NewTemperature { get; }
        public int OldTemperature { get; }
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