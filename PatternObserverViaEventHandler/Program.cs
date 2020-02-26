using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PatternObserverViaEventHandler
{
    static class Program
    {
        static void Main(string[] args)
        {
            //coздать объект класса Thermostat
            Thermostat thermostat = new Thermostat();

            Heater heater = new Heater(30,thermostat);

            Cooler cooler = new Cooler(40);

            //thermostat.TemperatureChanged += heater.OnTemperatureChanged;

            cooler.Register(thermostat);

            thermostat.EmulateTemperatureChange();

            //thermostat.TemperatureChanged = null;// CTE
            //thermostat.TemperatureChanged = heater.OnTemperatureChanged;// CTE
            cooler.UnRegister(thermostat);
            thermostat.EmulateTemperatureChange();
            //coздать объект класса Heater установив начальную температуру равную 30 градусов

            //coздать объект класса Cooler установив начальную температуру равную 40 градусов

            //объект класса Heater - подписаться на событие изменения температуры класса Thermostat

            //объект класса Cooler - подписаться на событие изменения температуры класса Thermostat

            //эмуляция изменения температуры объекта класса Thermostat

            //объект класса Cooler - отписаться от события изменения температуры класса Thermostat

            //эмуляция изменения температуры объекта класса Thermostat на 45 градусов

            Type type = thermostat.GetType();

            foreach (var t in type.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
            {
                Console.WriteLine(t.Name);
            }
        }
    }

    public class TemperatureChangedEventArgs : EventArgs
    {
        public int NewTemperature { get; }

        public TemperatureChangedEventArgs(int newTemperature)
        {
            NewTemperature = newTemperature;
        }
    }

    public class Cooler
    {
        public Cooler(int temperature) => Temperature = temperature;

        public void Register(Thermostat thermostat)
        {
            thermostat.TemperatureChanged += Update;
        }
        
        public void UnRegister(Thermostat thermostat)
        {
            thermostat.TemperatureChanged -= Update;
        }
        public int Temperature { get; private set; }

        public void Update(object sender, TemperatureChangedEventArgs info)
        {
            Console.WriteLine(info.NewTemperature > Temperature
                ? $"Cooler: On. Changed:{Math.Abs(info.NewTemperature - Temperature)}"
                : $"Cooler: Off. Changed:{Math.Abs(info.NewTemperature - Temperature)}");
        }
    }

    public class Heater
    {
        public Heater(int temperature, Thermostat thermostat)
        {
            thermostat.TemperatureChanged += OnTemperatureChanged;
            Temperature = temperature;
        }

        public int Temperature { get; private set; }

        public void OnTemperatureChanged(object sender, TemperatureChangedEventArgs info)
        {
            Console.WriteLine(info.NewTemperature < Temperature
                ? $"Heater: On. Changed:{Math.Abs(info.NewTemperature - Temperature)}"
                : $"Heater: Off. Changed:{Math.Abs(info.NewTemperature - Temperature)}");
        }
    }

    public sealed class Thermostat
    {
        private int currentTemperature;

        //private EventHandler<TemperatureChangedEventArgs> temperatureChanged;

        public event EventHandler<TemperatureChangedEventArgs> TemperatureChanged = delegate { };

        /* public void add_TemperatureChanged(EventHandler<TemperatureChangedEventArgs> handler)
         {
             temperatureChanged = 
                 (EventHandler<TemperatureChangedEventArgs>)Delegate.Combine(temperatureChanged, handler);
         }
         
         public void remove_TemperatureChanged(EventHandler<TemperatureChangedEventArgs> handler)
         {
             temperatureChanged = 
                 (EventHandler<TemperatureChangedEventArgs>)Delegate.Remove(temperatureChanged, handler);
         }*/

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
                    OnTemperatureChanged(new TemperatureChangedEventArgs(currentTemperature));
                }
            }
        }

        private void OnTemperatureChanged(TemperatureChangedEventArgs info)
        {
            var temp = TemperatureChanged;
                    
            temp?.Invoke(this, info);
        }

        public void EmulateTemperatureChange()
        {
            this.CurrentTemperature = random.Next(0, 100);
        }
    }
}