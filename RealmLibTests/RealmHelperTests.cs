using Microsoft.VisualStudio.TestTools.UnitTesting;
using RealmLib;
using Realms;
using System;
using System.Collections.Generic;

namespace RealmLibTests
{
    [TestClass]
    public class RealmHelperTests
    {
        [TestMethod]
        public void IsEntity()
        {
            Assert.IsTrue(RealmHelper.IsEntity(new BaseEntity1().GetType()));
            Assert.IsTrue(RealmHelper.IsEntity(new SubEntity1().GetType()));
            Assert.IsTrue(RealmHelper.IsEntity(new SubEntity2().GetType()));
            Assert.IsTrue(RealmHelper.IsEntity(new SubSubEntity1().GetType()));
            Assert.IsFalse(RealmHelper.IsEntity(new NotEntity().GetType()));
        }
        [TestMethod]
        public void IsSubEntity()
        {
            Assert.IsFalse(RealmHelper.IsSubEntity(new BaseEntity1().GetType()));
            Assert.IsFalse(RealmHelper.IsSubEntity(new SubEntity1().GetType()));
            Assert.IsFalse(RealmHelper.IsSubEntity(new SubEntity2().GetType()));
            Assert.IsFalse(RealmHelper.IsSubEntity(new SubSubEntity1().GetType()));
            Assert.IsFalse(RealmHelper.IsSubEntity(new NotEntity().GetType()));
            Assert.IsTrue(RealmHelper.IsSubEntity(new List<SubEntity1>().GetType()));
            Assert.IsFalse(RealmHelper.IsSubEntity(new List<NotEntity>().GetType()));
        }
        [TestMethod]
        public void resolve()
        {
            var entities1 = new List<SubEntity1>();
            entities1.Add(new SubEntity1());
            entities1.Add(new SubEntity1());
            BaseEntity1 element = new BaseEntity1()
            {
                sub1 = entities1,
                sub2 = new SubEntity2(),
                b1 = 11111
            };

            var list = new List<RealmObject>();
            RealmHelper.Resolve(element, list);
            Assert.AreEqual(3, list.Count);
        }
    }

    public class BaseEntity1 : RealmObject
    {
        public int b1 { get; set; }
        public List<SubEntity1> sub1 { get; set; }
        public NotEntity ne { get; set; }
        public SubEntity2 sub2 { get; set; }
    }

    public class SubEntity1 : RealmObject
    {
        int s1 { get; set; }
    }

    public class SubEntity2 : RealmObject
    {
        int s2 { get; set; }
        List<SubEntity1> subSub1 { get; set; }
    }
    public class SubSubEntity1 : RealmObject
    {
        int ss3 { get; set; }
    }
    public class NotEntity
    {
        int ne1 { get; set; }
    }


}

