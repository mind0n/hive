using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Threading;

namespace TestCeline
{
	[TestClass]
	public class test1
	{
		protected IWebDriver driver;

		[TestInitialize]
		public void Init()
		{
			driver = new ChromeDriver();
		}

		[TestMethod]
		public void TestBaidu()
		{
			driver.Url = "https://www.baidu.com";
			driver.Manage().Window.Maximize();
			try
			{
				var input = driver.FindElement(By.Id("kw"));
				input.SendKeys("celine");
				driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);

				//        //Employee[@id='4']
				var div = driver.FindElement(By.XPath("//div[@class='nums']"));
				//var div = driver.FindElement(By.ClassName("nums"));
				Assert.IsTrue(div.Text.Contains("百度为您找到相关结果约"));
				var screenshot = ((ITakesScreenshot)driver).GetScreenshot();
				screenshot.SaveAsFile("output.png", ScreenshotImageFormat.Png);
			}
			catch (Exception ex)
			{
				Assert.Fail("Cannot find search box");
			}
		}

		[TestCleanup]
		public void Cleanup()
		{
			driver.Quit();
		}
	}
}
