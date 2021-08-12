﻿using System;
using System.Collections.Generic;
using System.Reflection;

namespace Kernel.Domain.Model.ValueObjects
{
    // Classe escrita por Jimmy Bogard
    // http://grabbagoft.blogspot.com.br/2007/06/generic-value-object-equality.html
    public abstract class ValueObject<T> : IEquatable<T> where T : ValueObject<T>
    {
        public override bool Equals(object obj)
        {
            if (obj == null) return false;

            var other = obj as T;
            return Equals(other);
        }

        public override int GetHashCode()
        {
            var fields = GetFields();

            const int startValue = 17;
            const int multiplier = 59;

            var hashcode = startValue;
            foreach (var field in fields)
            {
                var value = field.GetValue(this);
                if (value != null)
                    hashcode = hashcode * multiplier + value.GetHashCode();
            }
            return hashcode;
        }

        public bool Equals(T other)
        {
            if (other == null) return false;

            var t = GetType();
            var otherType = other.GetType();

            if (t != otherType) return false;

            var fields = t.GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
            foreach (var field in fields)
            {
                var value1 = field.GetValue(other);
                var value2 = field.GetValue(this);

                if (value1 == null)
                {
                    if (value2 != null)
                        return false;
                }
                else if (!value1.Equals(value2))
                    return false;
            }
            return true;
        }

        private IEnumerable<FieldInfo> GetFields()
        {
            var t = GetType();
            var fields = new List<FieldInfo>();

            while (t != typeof(object))
            {
                fields.AddRange(t.GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public));
                t = t.BaseType;
            }
            return fields;
        }

        public static bool operator ==(ValueObject<T> x, ValueObject<T> y) => x != null && x.Equals(y);

        public static bool operator !=(ValueObject<T> x, ValueObject<T> y) => !(x == y);
    }
}