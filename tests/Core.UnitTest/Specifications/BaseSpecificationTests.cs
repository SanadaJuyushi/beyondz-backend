using System;
using System.Linq.Expressions;
using Core.Specifications;
using NUnit.Framework;

namespace Core.UnitTest.Specifications
{
    [TestFixture]
    public class BaseSpecificationTests
    {


        [SetUp]
        public void SetUp()
        {

        }

        [Test]
        public void BaseSpecification_PassArgcCiteria_PropertyCiteriaWillBeSet()
        {
            // Arrange
            Expression<Func<FakeEntity, bool>> expected = x => x.Name == "Tom";

            // Act
            var baseSpecification = new BaseSpecification<FakeEntity>(expected);
            var result = baseSpecification.Criteria;

            // Assert
            Assert.AreEqual(expected, result);
        }
    }
}
