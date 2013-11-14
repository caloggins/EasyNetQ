// ReSharper disable InconsistentNaming
using System;
using NUnit.Framework;

namespace EasyNetQ.Tests
{
    [TestFixture]
    public class TypeNameSerializerTests
    {
        const string expectedTypeName = "System.String:mscorlib";
        private const string expectedCustomTypeName = "EasyNetQ.Tests.SomeRandomClass:EasyNetQ.Tests";

        private ITypeNameSerializer typeNameSerializer;

        [SetUp]
        public void SetUp()
        {
            typeNameSerializer = new TypeNameSerializer();
        }

        [Test]
        public void Should_serialize_a_type_name()
        {
            var typeName = typeNameSerializer.Serialize(typeof(string));
            typeName.ShouldEqual(expectedTypeName);
        }

        [Test]
        public void Should_serialize_a_custom_type()
        {
            var typeName = typeNameSerializer.Serialize(typeof(SomeRandomClass));
            typeName.ShouldEqual(expectedCustomTypeName);
        }

        [Test]
        public void Should_deserialize_a_type_name()
        {
            var type = typeNameSerializer.DeSerialize(expectedTypeName);
            type.ShouldEqual(typeof (string));
        }

        [Test]
        public void Should_deserialize_a_custom_type()
        {
            var type = typeNameSerializer.DeSerialize(expectedCustomTypeName);
            type.ShouldEqual(typeof(SomeRandomClass));
        }

        [Test]
        [ExpectedException(typeof(EasyNetQException))]
        public void Should_throw_exception_when_type_name_is_not_recognised()
        {
            typeNameSerializer.DeSerialize("EasyNetQ.TypeNameSerializer.None:EasyNetQ");
        }

        public void Spike()
        {
            var type = Type.GetType("EasyNetQ.Tests.SomeRandomClass, EasyNetQ.Tests");
            type.ShouldEqual(typeof (SomeRandomClass));
        }

        public void Spike2()
        {
            var name = typeof (SomeRandomClass).AssemblyQualifiedName;
            Console.Out.WriteLine(name);
        }
    }

    public class SomeRandomClass
    {
        
    }
}
// ReSharper restore InconsistentNaming
