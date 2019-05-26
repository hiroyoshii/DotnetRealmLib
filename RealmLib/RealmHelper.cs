using Realms;
using System;
using System.Collections;
using System.Collections.Generic;

namespace RealmLib
{
    public class RealmHelper
    {
        public static void RemoveCascade(RealmObject entity, Realm db)
        {
            var toBeDeleted = new List<RealmObject>();
            toBeDeleted.Add(entity);
            while (toBeDeleted.Count > 0)
            {
                var element = toBeDeleted[0];
                toBeDeleted.RemoveAt(0);
                Resolve(element, toBeDeleted);
                db.Remove(element);
            }
        }

        public static void Resolve(RealmObject element, List<RealmObject> toBeDeleted)
        {
            foreach (var property in element.GetType().GetProperties())
            {
                if (IsEntity(property.PropertyType))
                {
                    toBeDeleted.Add((RealmObject)property.GetValue(element));
                }
                if (IsSubEntity(property.PropertyType))
                {
                    foreach (var value in property.GetValue(element) as IList)
                    {
                        toBeDeleted.Add(value as RealmObject);
                    }
                }
            }
        }

        public static bool IsEntity(Type type)
        {
            return type.IsSubclassOf(typeof(RealmObject));
        }

        public static bool IsSubEntity(Type type)
        {
            return type.IsGenericType
                && type.GetGenericTypeDefinition() == typeof(List<>)
                && type.GetGenericArguments()[0].IsSubclassOf(typeof(RealmObject));
        }

    }
}
