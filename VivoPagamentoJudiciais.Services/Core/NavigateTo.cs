using System;
using System.Collections.Generic;
using System.Text;
using VivoPagamentoJudiciais.Services.UIElement.Adquira;

namespace VivoPagamentoJudiciais.Services.Core
{
    public static class NavigateTo
    {
        public static void AdquiraLoginPage()
        {
            Driver.driver.Navigate().GoToUrl(Config.AdquiraPage.AdquiraLoginURL);
        }

        public static void AdquiraFaturaInternaPage()
        {
            Driver.driver.Navigate().GoToUrl(Config.AdquiraPage.AdquiraFaturaInternaURL);
        }

        public static void VivoPagtoPage()
        {
            Driver.driver.Navigate().GoToUrl(Config.VivoPagtoPage.VivoPagtoLoginURL);
        }

        public static void VivoPagtoFaturaInternaPage()
        {
            Driver.driver.Navigate().GoToUrl(Config.VivoPagtoPage.VivoPagtoFaturaInternaURL);
        }
    }
}
