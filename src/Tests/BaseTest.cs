using AutoMoq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    public class BaseTest
    {
        protected AutoMoqer automoqer;

        [TestInitialize]
        public void Initialize()
        {
            automoqer = new AutoMoqer();
        }
    }
}