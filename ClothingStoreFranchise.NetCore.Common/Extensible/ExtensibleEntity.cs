using ClothingStoreFranchise.NetCore.Common.Types;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace ClothingStoreFranchise.NetCore.Common.Extensible
{
    public interface IExtensibleEntity
    {
        string SerializedExtendedData { get; set; }

        /// <summary>
        /// Checks if the entity has the attribute with that name
        /// </summary>
        /// <param name="attrName"></param>
        /// <returns></returns>
        bool HasAttribute(string attributeName);

        /// <summary>
        /// Checks whether the entity has any extensible attribute
        /// </summary>
        /// <returns></returns>
        bool HasAnyAttribute();

        /// <summary>
        /// Gets an specific attribute value
        /// </summary>
        /// <param name="attributeName">The requested attribute name</param>
        /// <returns></returns>
        ExtendedValue GetExtendedValue(string attributeName);

        /// <summary>
        /// Takes the collection of attributes and serializes them
        /// </summary>
        void SerializeExtendedAttributes();

        Dictionary<string, ExtendedValue> ExtendedData { get; }
    }

    public abstract class ExtensibleEntity<K> : Entity<K>, IExtensibleEntity
    {
        [NotMapped]
        public Dictionary<string, ExtendedValue> ExtendedData { get; private set; }
            = new Dictionary<string, ExtendedValue>();

        [NotMapped]
        public string SerializedExtendedData { get; set; }

        public bool HasAttribute(string attributeName)
        {
            return ExtendedData.Keys
                .Select(atrrName => atrrName.ToUpperInvariant()).Contains(attributeName.ToUpperInvariant().Trim());
        }

        public bool HasAnyAttribute()
        {
            return !string.IsNullOrEmpty(SerializedExtendedData)
                || ExtendedData.Any();
        }

        public ExtendedValue GetExtendedValue(string attributeName)
        {
            ExtendedData.TryGetValue(attributeName, out var value);
            return value;
        }

        public void SerializeExtendedAttributes()
        {
            SerializedExtendedData = Serialize(ExtendedData);
        }

        #region "Private methods"

        private static string Serialize(Dictionary<string, ExtendedValue> value)
        {
            return JsonConvert.SerializeObject(value);
        }

        private static Dictionary<string, ExtendedValue> Deserialize(string value)
        {
            return JsonConvert.DeserializeObject<Dictionary<string, ExtendedValue>>(value);
        }

        #endregion
    }
}
