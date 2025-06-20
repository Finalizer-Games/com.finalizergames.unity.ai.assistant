using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using OpenAPIDateConverter = Unity.Ai.Assistant.Protocol.Client.OpenAPIDateConverter;
using System.Reflection;

namespace Unity.Ai.Assistant.Protocol.Model
{
    /// <summary>
    /// ValidationErrorLocInner
    /// </summary>
    [JsonConverter(typeof(ValidationErrorLocInnerJsonConverter))]
    [DataContract(Name = "ValidationError_loc_inner")]
    internal partial class ValidationErrorLocInner : AbstractOpenAPISchema
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationErrorLocInner" /> class
        /// with the <see cref="string" /> class
        /// </summary>
        /// <param name="actualInstance">An instance of string.</param>
        public ValidationErrorLocInner(string actualInstance)
        {
            this.IsNullable = false;
            this.SchemaType= "anyOf";
            this.ActualInstance = actualInstance ?? throw new ArgumentException("Invalid instance found. Must not be null.");
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationErrorLocInner" /> class
        /// with the <see cref="int" /> class
        /// </summary>
        /// <param name="actualInstance">An instance of int.</param>
        public ValidationErrorLocInner(int actualInstance)
        {
            this.IsNullable = false;
            this.SchemaType= "anyOf";
            this.ActualInstance = actualInstance;
        }


        private Object _actualInstance;

        /// <summary>
        /// Gets or Sets ActualInstance
        /// </summary>
        public override Object ActualInstance
        {
            get
            {
                return _actualInstance;
            }
            set
            {
                if (value.GetType() == typeof(int))
                {
                    this._actualInstance = value;
                }
                else if (value.GetType() == typeof(string))
                {
                    this._actualInstance = value;
                }
                else
                {
                    throw new ArgumentException("Invalid instance found. Must be the following types: int, string");
                }
            }
        }

        /// <summary>
        /// Get the actual instance of `string`. If the actual instance is not `string`,
        /// the InvalidClassException will be thrown
        /// </summary>
        /// <returns>An instance of string</returns>
        public string GetString()
        {
            return (string)this.ActualInstance;
        }

        /// <summary>
        /// Get the actual instance of `int`. If the actual instance is not `int`,
        /// the InvalidClassException will be thrown
        /// </summary>
        /// <returns>An instance of int</returns>
        public int GetInt()
        {
            return (int)this.ActualInstance;
        }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class ValidationErrorLocInner {\n");
            sb.Append("  ActualInstance: ").Append(this.ActualInstance).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }

        /// <summary>
        /// Returns the JSON string presentation of the object
        /// </summary>
        /// <returns>JSON string presentation of the object</returns>
        public override string ToJson()
        {
            return JsonConvert.SerializeObject(this.ActualInstance, ValidationErrorLocInner.SerializerSettings);
        }

        /// <summary>
        /// Converts the JSON string into an instance of ValidationErrorLocInner
        /// </summary>
        /// <param name="jsonString">JSON string</param>
        /// <returns>An instance of ValidationErrorLocInner</returns>
        public static ValidationErrorLocInner FromJson(string jsonString)
        {
            ValidationErrorLocInner newValidationErrorLocInner = null;

            if (string.IsNullOrEmpty(jsonString))
            {
                return newValidationErrorLocInner;
            }

            try
            {
                newValidationErrorLocInner = new ValidationErrorLocInner(JsonConvert.DeserializeObject<int>(jsonString, ValidationErrorLocInner.SerializerSettings));
                // deserialization is considered successful at this point if no exception has been thrown.
                return newValidationErrorLocInner;
            }
            catch (Exception exception)
            {
                // deserialization failed, try the next one
                System.Diagnostics.Debug.WriteLine(string.Format("Failed to deserialize `{0}` into int: {1}", jsonString, exception.ToString()));
            }

            try
            {
                newValidationErrorLocInner = new ValidationErrorLocInner(JsonConvert.DeserializeObject<string>(jsonString, ValidationErrorLocInner.SerializerSettings));
                // deserialization is considered successful at this point if no exception has been thrown.
                return newValidationErrorLocInner;
            }
            catch (Exception exception)
            {
                // deserialization failed, try the next one
                System.Diagnostics.Debug.WriteLine(string.Format("Failed to deserialize `{0}` into string: {1}", jsonString, exception.ToString()));
            }

            // no match found, throw an exception
            throw new InvalidDataException("The JSON string `" + jsonString + "` cannot be deserialized into any schema defined.");
        }

    }

    /// <summary>
    /// Custom JSON converter for ValidationErrorLocInner
    /// </summary>
    internal class ValidationErrorLocInnerJsonConverter : JsonConverter
    {
        /// <summary>
        /// To write the JSON string
        /// </summary>
        /// <param name="writer">JSON writer</param>
        /// <param name="value">Object to be converted into a JSON string</param>
        /// <param name="serializer">JSON Serializer</param>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteRawValue((string)(typeof(ValidationErrorLocInner).GetMethod("ToJson").Invoke(value, null)));
        }

        /// <summary>
        /// To convert a JSON string into an object
        /// </summary>
        /// <param name="reader">JSON reader</param>
        /// <param name="objectType">Object type</param>
        /// <param name="existingValue">Existing value</param>
        /// <param name="serializer">JSON Serializer</param>
        /// <returns>The object converted from the JSON string</returns>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            switch(reader.TokenType)
            {
                case JsonToken.String:
                    return new ValidationErrorLocInner(Convert.ToString(reader.Value));
                case JsonToken.Integer:
                    return new ValidationErrorLocInner(Convert.ToInt32(reader.Value));
                case JsonToken.StartObject:
                    return ValidationErrorLocInner.FromJson(JObject.Load(reader).ToString(Formatting.None));
                case JsonToken.StartArray:
                    return ValidationErrorLocInner.FromJson(JArray.Load(reader).ToString(Formatting.None));
                default:
                    return null;
            }
        }

        /// <summary>
        /// Check if the object can be converted
        /// </summary>
        /// <param name="objectType">Object type</param>
        /// <returns>True if the object can be converted</returns>
        public override bool CanConvert(Type objectType)
        {
            return false;
        }
    }

}
