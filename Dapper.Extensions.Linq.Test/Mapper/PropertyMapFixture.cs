using System;
using System.Linq.Expressions;
using System.Reflection;
using Dapper.Extensions.Linq.Core.Enums;
using Dapper.Extensions.Linq.Mapper;
using NUnit.Framework;

namespace Dapper.Extensions.Linq.Test.Mapper
{
    [TestFixture]
    public class PropertyMapFixture
    {
        private class Foo
        {
            public int Bar { get; set; }
            public string Baz { get; set; }
        }

        [Test]
        public void PropertyMap_Constructor_Sets_Name_And_ColumnName_Property_From_PropertyInfo()
        {
            Expression<Func<Foo, object>> expression = f => f.Bar;
            var pi = ReflectionHelper.GetProperty(expression) as PropertyInfo;
            PropertyMap pm = new PropertyMap(pi);
            Assert.AreEqual("Bar", pm.Name);
            Assert.AreEqual("Bar", pm.ColumnName);
        }

        [Test]
        public void PropertyMap_Column_Sets_ColumnName_But_Does_Not_Change_Name()
        {
            Expression<Func<Foo, object>> expression = f => f.Baz;
            var pi = ReflectionHelper.GetProperty(expression) as PropertyInfo;
            PropertyMap pm = new PropertyMap(pi);
            pm.Column("X");
            Assert.AreEqual("Baz", pm.Name);
            Assert.AreEqual("X", pm.ColumnName);
        }

        [Test]
        public void PropertyMap_Key_Sets_KeyType()
        {
            Expression<Func<Foo, object>> expression = f => f.Baz;
            var pi = ReflectionHelper.GetProperty(expression) as PropertyInfo;
            PropertyMap pm = new PropertyMap(pi);
            pm.Column("X").Key(KeyType.Identity);
            Assert.AreEqual("Baz", pm.Name);
            Assert.AreEqual("X", pm.ColumnName);
            Assert.AreEqual(KeyType.Identity, pm.KeyType);
        }
    }
}