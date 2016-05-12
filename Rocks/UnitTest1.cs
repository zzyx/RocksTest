using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;

namespace Rocks
{
    [TestFixture]
    public class NUnitTest
    {
        IWebDriver driver;

        [SetUp]
        public void Inintialize()
        {
            driver = new FirefoxDriver();
            //driver = new ChromeDriver() ;

            driver.Manage().Window.Maximize();
            //TimeSpan t  = new TimeSpan(0,0,60);
            //driver.Manage().Timeouts().ImplicitlyWait(t);
            driver.Url = "http://www.radioroks.ua/";
        }


        [Test]
        public void OpenAppTest()
        {
            

            const string title = "Radio ROKS. Рок. Тільки рок!";
            var actualTile = driver.Title;

            Assert.That(actualTile, Is.EqualTo(title));
        }

        [Test]
        public void KamtugesaExist()
        {
            var element = driver.FindElement(By.ClassName("kamtugeza-btn"));
            Assert.That(element.Displayed);

            element.Click();


        }

        [Test]
        public void GetNames()
        {
            const string dateXpath = @"//*[@id='caption-hp-player' and ./a/span[contains(text(),'9:45')] ]/span";
            const string textXpath = @"//*[@id='caption-hp-player' and ./a/span[contains(text(),'9:45')] ]/div[@class='about']";
            const string prevButton = @"//a[@title='Prev' ]";
            const string selecDate = @"//*[@class='ui-datepicker-calendar']//*[@onclick]/a[text()='{0}']";

            for (var i = 0; i < 2; i++)
            {
                var dates = driver.FindElements(By.CssSelector(".ui-datepicker-calendar [onclick] a")).ToList();
                var listeList = dates.Select(webElement => webElement.Text).ToList();
                if (listeList.Capacity == 0) driver.FindElement(By.XPath(prevButton)).Click();
                else
                {
                    foreach (var textDate in listeList.Select(item => item.ToString()))
                    {
                        try
                        {
                            driver.FindElement(By.XPath(string.Format(selecDate, textDate))).Click();
                            var text = driver.FindElement(By.XPath(textXpath)).Text;
                            var date = driver.FindElement(By.XPath(dateXpath)).Text;
                            Console.WriteLine("{0} : {1}", date, text);
                        }
                        finally
                        {
                        }
                    }
                    driver.FindElement(By.XPath(prevButton)).Click();
                }

            }
        }
        [TearDown]
        public void EndTest()
        {
            driver.Close();
        }
    }
}
