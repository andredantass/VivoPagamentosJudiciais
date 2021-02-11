using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;
using VivoPagamentoJudiciais.Service.Core;

namespace VivoPagamentoJudiciais.Service.UIElement
{
    public class VivoPagtoFaturaInternaPage
    {

        public IWebElement fieldNomeModelo => Driver.driver.FindElement(By.Id("cbModelos_chosen"));
        public IWebElement fieldAreaReclamacao => Driver.driver.FindElement(By.Id("FiltroAreaReclamacao"));
        public IWebElement formUploadFile => Driver.driver.FindElement(By.Id("dropzoneForm"));
        
        public VivoPagtoFaturaInternaPage()
        {

        }
    }
}
