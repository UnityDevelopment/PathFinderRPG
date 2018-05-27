namespace PathfinderRPG.Utilities
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    public static class ReflectiveEnumerator
    {
        /// <summary>
        /// Initialises static members of the <see cref="ReflectiveEnumerator" /> class
        /// </summary>
        static ReflectiveEnumerator()
        {
        }

        /// <summary>
        /// Gets an IEnumerable of the specified T
        /// </summary>
        /// <typeparam name="T">The type</typeparam>
        /// <param name="constructorArgs">The constructor arguments</param>
        /// <returns>An IEnumerable of the specified type/></returns>
        public static IEnumerable<T> GetEnumerableOfType<T>(params object[] constructorArgs) where T : class, IComparable<T>
        {
            List<T> objects = new List<T>();
            foreach (Type type in 
                Assembly.GetAssembly(typeof(T)).GetTypes()
                .Where(myType => myType.IsClass && !myType.IsAbstract && myType.IsSubclassOf(typeof(T))))
            {
                objects.Add((T)Activator.CreateInstance(type, constructorArgs));
            }      

            objects.Sort();
            return objects;
        }
    }
}