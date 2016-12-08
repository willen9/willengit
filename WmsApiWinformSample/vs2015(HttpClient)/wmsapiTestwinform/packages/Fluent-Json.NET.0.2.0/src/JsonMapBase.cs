using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Reflection;
using JetBrains.Annotations;

namespace FluentJsonNet
{
    public abstract class JsonMapBase
    {
        public List<Action<MemberInfo, JsonProperty, MemberSerialization>> Actions { get; } =
            new List<Action<MemberInfo, JsonProperty, MemberSerialization>>();

        private Dictionary<object, object> data;

        /// <summary>
        /// Gets the type of the class being mapped.
        /// </summary>
        public abstract Type SerializedType { get; }

        /// <summary>
        /// Gets a collection of dynamic data that is associated with an object.
        /// </summary>
        public dynamic GetData(object obj)
        {
            if (this.data == null)
                this.data = new Dictionary<object, object>();
            dynamic d;
            if (!this.data.TryGetValue(obj, out d))
                this.data[obj] = new ExpandoObject();
            return d;
        }

        /// <summary>
        /// Gets an arbitrary data object that is associated with an object.
        /// </summary>
        public T GetData<T>(object obj, [NotNull] Func<T> creator)
        {
            if (creator == null)
                throw new ArgumentNullException(nameof(creator));

            if (this.data == null)
                this.data = new Dictionary<object, object>();
            dynamic d;
            if (!this.data.TryGetValue(obj, out d))
                this.data[obj] = creator();
            return d;
        }

        /// <summary>
        /// Gets the a value indicating whether the type can be processed by this class.
        /// </summary>
        public virtual bool AcceptsType(Type objectType)
        {
            return objectType == this.SerializedType;
        }

        public virtual bool AcceptsMember(MemberInfo member, MemberInfo memberInfo)
        {
            return member.MetadataToken == memberInfo.MetadataToken
                   && member.DeclaringType == memberInfo.DeclaringType;
        }

        internal string DiscriminatorFieldName;

        /// <summary>
        /// Defines the name of the field that is used to discriminate
        ///  subclasses of the type being currently mapped.
        /// </summary>
        /// <param name="fieldName"></param>
        protected void DiscriminateSubClassesOnField(string fieldName)
        {
            this.DiscriminatorFieldName = fieldName;
        }

        public abstract object CreateNew();

        public abstract bool CanCreateNew();
    }
}