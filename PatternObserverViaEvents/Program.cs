using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PatternObserverViaEvents
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
        //TODO
    }

    public class Heater
    {
        //TODO
    }

    public sealed class Thermostat
    {
        //TODO
    }
}
