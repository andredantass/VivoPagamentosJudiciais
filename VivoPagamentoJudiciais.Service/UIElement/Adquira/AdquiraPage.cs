﻿using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using System;
using System.Collections.Generic;
using System.Text;
using VivoPagamentoJudiciais.Service.Core;

namespace VivoPagamentoJudiciais.Service.UIElement
{
    public class AdquiraPage
    {
     
        public IWebElement fieldUserName => Driver.driver.FindElement(By.Name("j_username"));
        public IWebElement fieldPassword => Driver.driver.FindElement(By.Name("j_password"));
        public IWebElement btnLogin => Driver.driver.FindElement(By.Id("login_form_submit_button"));

      
      
    }
}
