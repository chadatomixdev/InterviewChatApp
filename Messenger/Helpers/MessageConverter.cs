using System;
using Messenger.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;

namespace Messenger.Helpers
{
    public class MessageConverter : CustomCreationConverter<IMessage>
    {
        public override IMessage Create(Type objectType)
        {
            throw new NotImplementedException();
        }

        public IMessage Create(Type objectType, JObject jObject)
        {
            var type = (string)jObject.Property("mtype");

            switch (type)
            {
                case "text_message":
                    return new TextMessage();
                case "image_message":
                    return new ImageMessage();
                case "user_registered":
                    return new UserRegistration();
                case "group_created":
                    return new Group();
                case "delivery_report":
                    return new DeliveryReport();
            }

            throw new ApplicationException(String.Format("The message type {0} is not supported!", type));
        }

    	public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JObject jObject = JObject.Load(reader);

            var target = Create(objectType, jObject);

            serializer.Populate(jObject.CreateReader(), target);

            return target;
        }
    }
}