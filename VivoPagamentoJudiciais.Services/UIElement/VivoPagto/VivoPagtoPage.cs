using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;
using VivoPagamentoJudiciais.Services.Core;

namespace VivoPagamentoJudiciais.Services.UIElement.VivoPagto
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
