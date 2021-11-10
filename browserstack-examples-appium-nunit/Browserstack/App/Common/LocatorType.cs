using System;
using OpenQA.Selenium.Appium;

namespace BrowserStack.App.Common
{
    public enum LocatorType
    {
        AccessibilityId,
        XPath,
        ClassName,
        AndroidUIAutomator2,
        IOSClassChain,
        IOSPredicateString,
        Id,
        Name
    }

    public class FindElements : TechnicalActions
    {

        public FindElements(AppiumDriver<AppiumWebElement> app): base(app)
        {
            App = app;
        }

        public AppiumWebElement FindElement(LocatorType locatorType, string locatorValue)
        {
            switch (locatorType)
            {
                case LocatorType.AccessibilityId:
                    return WaitForElement(MobileBy.AccessibilityId(locatorValue));
                case LocatorType.ClassName:
                    return WaitForElement(MobileBy.ClassName(locatorValue));
                case LocatorType.XPath:
                    return WaitForElement(MobileBy.XPath(locatorValue));
                case LocatorType.Name:
                    return WaitForElement(MobileBy.Name(locatorValue));
                case LocatorType.Id:
                    return WaitForElement(MobileBy.Id(locatorValue));
                case LocatorType.AndroidUIAutomator2:
                    return WaitForElement(MobileBy.AndroidUIAutomator(locatorValue));
                case LocatorType.IOSPredicateString:
                    return WaitForElement(MobileBy.IosNSPredicate(locatorValue));
                case LocatorType.IOSClassChain:
                    return WaitForElement(MobileBy.IosClassChain(locatorValue));
                default:
                    return WaitForElement(MobileBy.AccessibilityId(locatorValue));
            }
        }

    }

}
   