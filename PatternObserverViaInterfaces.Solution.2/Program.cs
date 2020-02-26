using System;
using System.Collections.Generic;
using System.Threading;

namespace PatternObserverViaInterfaces.Solution._2
{
    static class Program
    {
        static void Main(string[] args)
        {
            Thermostat thermostat = new Thermostat();

            Heater heater = new Heater(30);

            Cooler cooler = new Cooler(40);

            //thermostat.Register(heater);
            
            //thermostat.Register(cooler);
            
            thermostat.EmulateTemperatureChange();

            //thermostat.Unregister(cooler);
            
            thermostat.EmulateTemperatureChange();

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
        void Update(IObservable sender, int info);
    }

    //интерфейс наблюдаемого класса 
    public interface IObservable
    {
        void Register(IObserver observer);
        void Unregister(IObserver observer);
        void Notify();
    }

    public class Cooler  : IObserver
    {
        public Cooler(int temperature)
        {
            Temperature = temperature;
        }

        public float Temperature { get; private set; }

        public void Update(IObservable sender, int newTemperature)
        {
            if (newTemperature > Temperature)
            {
                Console.WriteLine($"Cooler: On. Changed:{Math.Abs(newTemperature - Temperature)}");
            }
            else
            {
                Console.WriteLine($"Cooler: Off. Changed:{Math.Abs(newTemperature - Temperature)}");
            }
        }
    }

    public class Heater  : IObserver
    {
        public Heater(int temperature)
        {
            Temperature = temperature;
        }

        public float Temperature { get; private set; }

        public void Update(IObservable sender, int newTemperature)
        {
            if (newTemperature < Temperature)
            {
                Console.WriteLine($"Heater: On. Changed:{Math.Abs(newTemperature - Temperature)}");
            }
            else
            {
                Console.WriteLine($"Heater: Off. Changed:{Math.Abs(newTemperature - Temperature)}");
            }
        }
    }

    public class Thermostat : IObservable
    {
        private int currentTemperature;

        private Random random = new Random(Environment.TickCount);
        
        private readonly List<IObserver> observers;
        
        public Thermostat()
        {
            observers = new List<IObserver>();
        }
        
        public void Register(IObserver observer)
        {
            observers.Add(observer);
        }

        public void Unregister(IObserver observer)
        {
            observers.Remove(observer);
        }

        public void Notify()
        {
            foreach (var observer in observers)
            {
                observer.Update(this, currentTemperature);
            }
        }
        
        public int CurrentTemperature
        {
            get => currentTemperature;
            private set
            {
                if (value != currentTemperature)
                {
                    currentTemperature = value;
                    Notify();
                }
            }
        }

        public void EmulateTemperatureChange()
        {
            this.CurrentTemperature = random.Next(0, 100);
        }
    }
}