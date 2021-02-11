using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;
using VivoPagamentoJudiciais.Service.Core;

namespace VivoPagamentoJudiciais.Service.UIElement
{
    public class VivoPagtoPage
    {
        public IWebElement fieldUserName => Driver.driver.FindElement(By.Id("UserName"));
        public IWebElement fieldPassword => Driver.driver.FindElement(By.Id("Password"));
        public IWebElement btnLogin => Driver.driver.FindElement(By.CssSelector("body > div.row > div > div > form > div.form-group > div > button"));
        public VivoPagtoPage()
        {
           
        }
    }
}
