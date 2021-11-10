using System;
using System.Collections.Generic;
using System.Threading;
using BrowserStack.WebDriver.Config;
using log4net;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Support.UI;


namespace BrowserStack.App.Common
{
    public class TechnicalActions
    {
        protected AppiumDriver<AppiumWebElement> App;
        private static readonly ILog Log = LogManager.GetLogger(typeof(TechnicalActions));

        public TechnicalActions(AppiumDriver<AppiumWebElement> app)
        {
            App = app;
        }


        internal string GetText(Dictionary<DeviceType, PageObject> element)
        {
            AppiumWebElement appiumWebElement = FindElementByPlatform(element);
            var attributName = IsAndroid() ? "text" : "value";
            return appiumWebElement.GetAttribute(attributName);
        }

        internal void PerformClick(Dictionary<DeviceType, PageObject> element)
        {
            AppiumWebElement appiumWebElement = FindElementByPlatform(element);
            appiumWebElement.Click();
        }

        internal void SetText(Dictionary<DeviceType, PageObject> element, String text)
        {
            AppiumWebElement appiumWebElement = FindElementByPlatform(element);
            appiumWebElement.SendKeys(text);
        }

        internal bool ElementExists(Dictionary<DeviceType, PageObject> pageElement, string dynamicLocatorStr)
        {

            AppiumWebElement appiumWebElement = FindElementByPlatform(pageElement, dynamicLocatorStr);
            return appiumWebElement != null;
        }

        private bool IsAndroid()
        {
            return App
                .Capabilities
                .GetCapability("os")
                .ToString()
                .ToLower()
                .Equals(DeviceType.Android.ToString().ToLower());
        }


        public AppiumWebElement WaitForElement(By by, int duration = 30, bool forceFail = true)
        {
            AppiumWebElement webElement = null;
            try
            {
                WebDriverWait wait = new(App, TimeSpan.FromSeconds(duration));
                webElement = App.FindElement(by);
                wait.Until(c => webElement.Displayed);
            }
            catch (Exception e)
            {
                Log.Debug(e.Message);
                Log.Debug(App.PageSource);
                if(forceFail)
                {
                    throw new Exception("EXCEPTION: Could not find Web element - " +
                    e.Message);
                }
                
            }
            return webElement;
        }

        private AppiumWebElement FindElementByPlatform(Dictionary<DeviceType, PageObject> element, string dynamicLocatorValue = "")
        {
            AppiumWebElement appiumWebElement = null;
            PageObject objectLocator = null;
            string locatorValue;

            if (IsAndroid()) {
                objectLocator = element.GetValueOrDefault(DeviceType.Android);
            }

            else {
                objectLocator = element.GetValueOrDefault(DeviceType.Ios);
            }

            locatorValue = objectLocator.LocatorValue.ToString().Replace("<dynamic>", dynamicLocatorValue);
            FindElements findElements = new FindElements(App);
            appiumWebElement = findElements.FindElement(objectLocator.LocatorType, locatorValue);
            return appiumWebElement;
        }

       
    }
}
