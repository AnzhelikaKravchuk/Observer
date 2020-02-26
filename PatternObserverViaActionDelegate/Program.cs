using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatternObserverViaActionDelegate
{
    static class Program
    {
        static void Main(string[] args)
        {
            Thermostat thermostat = new Thermostat();

            Heater heater = new Heater(30);

            Cooler cooler = new Cooler(40);

            thermostat.observers += heater.OnTemperatureChanged;
            
            thermostat.observers += cooler.Update;
            
            thermostat.EmulateTemperatureChange();

            thermostat.observers = null;
            thermostat.observers = heater.OnTemperatureChanged;
            
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

    public class Cooler
    {
        public Cooler(int temperature) => Temperature = temperature;

        public int Temperature { get; private set; }

        public void Update(int newTemperature)
        {
            Console.WriteLine(newTemperature > Temperature
                ? $"Cooler: On. Changed:{Math.Abs(newTemperature - Temperature)}"
                : $"Cooler: Off. Changed:{Math.Abs(newTemperature - Temperature)}");
        }
    }

    public class Heater
    {
        public Heater(int temperature) => Temperature = temperature;

        public int Temperature { get; private set; }

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
        
        public Action<int> observers;
        
        private Random random = new Random(Environment.TickCount);

        public Thermostat()
        {
            currentTemperature = 5;
        }

        public int CurrentTemperature
        {
            get => currentTemperature;
            private set
            {
                if (value != currentTemperature)
                {
                    currentTemperature = value;
                    observers?.Invoke(currentTemperature);
                }
            }
        }

        public void EmulateTemperatureChange()
        {
            this.CurrentTemperature = random.Next(0, 100);
        }      
    }
}